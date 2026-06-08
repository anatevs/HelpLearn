using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using Network.UI;

namespace GameManagement
{

    public class LobbyManager : NetworkManager
    {
        public event Action<LobbyPlayer> OnLobbyPlayerRemoved;

        public event Action<int, string> OnNameChanged;
        public event Action<int, Color> OnColorChanged;
        public event Action<int, string, Color, bool> OnPlayerUpdated;
        public event Action<int, bool> OnReadyChanged;
        public event Action<int> OnPlayerRemoved;

        public IReadOnlyList<LobbyPlayer> Players => _players;

        public MultiplayerSettingsConfig MultiplayerSettingsConfig => _settingsConfig;

        [SerializeField]
        private LobbyHUDPresenter _lobbyHudPresenter;

        [SerializeField]
        private LobbyPlayerSettingsPresenter _settingsPresenter;

        [SerializeField]
        private MultiplayerSettingsConfig _settingsConfig;

        [SerializeField]
        [Tooltip("Prefab to use for the Room Player")]
        private LobbyPlayer _lobbyPlayerPrefab;

        [Header("Scenes")]
        /// <summary>
        /// The scene to use for the room. This is similar to the offlineScene of the NetworkManager.
        /// </summary>
        [Scene]
        public string RoomScene;

        /// <summary>
        /// The scene to use for the playing the game from the room. This is similar to the onlineScene of the NetworkManager.
        /// </summary>
        [Scene]
        public string GameplayScene;

        private List<LobbyPlayer> _players = new();
        private Stack<int> _freeSlots = new();

        private bool _showStartButton;


        public override void OnValidate()
        {
            base.OnValidate();

            if (_lobbyPlayerPrefab != null)
            {
                NetworkIdentity identity = _lobbyPlayerPrefab.GetComponent<NetworkIdentity>();
                if (identity == null)
                {
                    _lobbyPlayerPrefab = null;
                    Debug.LogError("RoomPlayer prefab must have a NetworkIdentity component.");
                }
            }
        }

        public override void Awake()
        {
            base.Awake();

            _lobbyHudPresenter.Init(this);
            _settingsPresenter.Init(_settingsConfig);

            for (int i = _settingsConfig.MaxPlayers - 1; i >= 0; i--)
            {
                _freeSlots.Push(i);
            }
        }

        public void ChangeName(int index, string name)
        {
            OnNameChanged?.Invoke(index, name);
        }

        public void ChangeColor(int index, Color color)
        {
            OnColorChanged?.Invoke(index, color);
        }

        public void ChangeReady(int index, bool isReady)
        {
            OnReadyChanged?.Invoke(index, isReady);
        }

        //overrides of NetworkManager

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            if (Utils.IsSceneActive(RoomScene) && _players.Count <= _settingsConfig.MaxPlayers)
            {
                //allPlayersReady = false;

                var lobbyPlayer = Instantiate(_lobbyPlayerPrefab, Vector3.zero, Quaternion.identity);
                //NetworkServer.AddPlayerForConnection(conn, lobbyPlayer.gameObject);

                lobbyPlayer.PlayerID = _freeSlots.Pop();
                lobbyPlayer.Init(GetDefaultName(lobbyPlayer.PlayerID));

                lobbyPlayer.OnNameChanged += ChangeName;
                lobbyPlayer.OnColorChanged += ChangeColor;
                lobbyPlayer.OnReadyChanged += ChangeReady;

                NetworkServer.AddPlayerForConnection(conn, lobbyPlayer.gameObject);

                foreach (var player in _players)
                {
                    OnPlayerUpdated?.Invoke(player.PlayerID, player.Name, player.Color, player.ReadyToBegin);
                }

                _players.Add(lobbyPlayer);
            }
            else
            {
                // Late joiners not supported...should've been kicked by OnServerDisconnect
                Debug.Log($"Not in Room scene or players amount exceeded...disconnecting {conn}");
                conn.Disconnect();
            }
        }

        public override void OnStartClient()
        {
            if (_lobbyPlayerPrefab == null || _lobbyPlayerPrefab.gameObject == null)
                Debug.LogError("NetworkRoomManager no RoomPlayer prefab is registered. Please add a RoomPlayer prefab.");
            else
                NetworkClient.RegisterPrefab(_lobbyPlayerPrefab.gameObject);

            if (playerPrefab == null)
                Debug.LogError("NetworkRoomManager no GamePlayer prefab is registered. Please add a GamePlayer prefab.");
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            // cannot join game in progress
            if (!Utils.IsSceneActive(RoomScene))
            {
                Debug.Log($"Not in Room scene...disconnecting {conn}");
                conn.Disconnect();
                return;
            }

            base.OnServerConnect(conn);
        }


        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            if (conn.identity != null)
            {
                if (conn.identity.TryGetComponent<LobbyPlayer>(out var disconnectedPlayer))
                {
                    OnLobbyPlayerRemoved?.Invoke(disconnectedPlayer);

                    _players.Remove(disconnectedPlayer);

                    disconnectedPlayer.OnNameChanged -= ChangeName;
                    disconnectedPlayer.OnColorChanged -= ChangeColor;
                    disconnectedPlayer.OnReadyChanged -= ChangeReady;

                    for (int i = disconnectedPlayer.PlayerID; i < _players.Count; i++)
                    {
                        var newName = _players[i].Name;

                        if (_players[i].Name == GetDefaultName(i + 1))
                        {
                            newName = GetDefaultName(i);
                            _players[i].Name = newName;
                        }

                        _players[i].PlayerID = i;

                        OnPlayerUpdated?.Invoke(_players[i].PlayerID, _players[i].Name, _players[i].Color, _players[i].ReadyToBegin);
                    }

                    var clearedIndex = _players.Count;

                    OnPlayerRemoved?.Invoke(clearedIndex);

                    _freeSlots.Push(clearedIndex);
                }
            }

            //allPlayersReady = false;

            //foreach (NetworkRoomPlayer player in roomSlots)
            //{
            //    if (player != null)
            //        player.GetComponent<NetworkRoomPlayer>().readyToBegin = false;
            //}

            //if (Utils.IsSceneActive(RoomScene))
            //    RecalculateRoomPlayerIndices();

            base.OnServerDisconnect(conn);

            // Restart the server if we're headless and no players are connected.
            // This will send server to offline scene, where auto-start will run.
            if (Utils.IsHeadless() && numPlayers < 1)
                StopServer();
        }


        private string GetDefaultName(int index)
        {
            return $"{_settingsConfig.DefaultNamePrefix}{index}";
        }


        public void OnRoomServerPlayersReady()
        {
            if (Utils.IsHeadless())
            {
                ServerChangeScene(GameplayScene);
            }
            else
            {
                _showStartButton = true;
            }
        }

        public bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            if (roomPlayer.TryGetComponent<LobbyPlayer>(out var lobbyPlayer))
            {
                OnLobbyPlayerRemoved?.Invoke(lobbyPlayer);

                if (gamePlayer.TryGetComponent<NetworkPlayer>(out var player))
                {
                    player.Name = lobbyPlayer.Name;
                    player.Color = lobbyPlayer.Color;
                }
            }

            return true;
        }

        //public override void OnGUI()
        //{
        //    base.OnGUI();

        //    if (allPlayersReady && _showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
        //    {
        //        _showStartButton = false;

        //        ServerChangeScene(GameplayScene);
        //    }
        //}
    }
}