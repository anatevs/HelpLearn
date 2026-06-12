using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using Network.UI;
using static Mirror.NetworkRoomManager;
using System.Linq;

namespace GameManagement
{

    public class LobbyManager : NetworkManager
    {
        public event Action<int, string> OnNameChanged;
        public event Action<int, Color> OnColorChanged;
        public event Action<int, string, Color, bool, bool> OnSlotUpdated;
        public event Action<int, bool> OnReadyChanged;
        public event Action<int, bool> OnPlayerAdded;
        public event Action<int> OnSlotEmptied;

        public event Action<bool> OnCanStartChanged;

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

        /// <summary>
        /// List of players that are in the Room
        /// </summary>
        private readonly HashSet<PendingPlayer> _pendingPlayers = new HashSet<PendingPlayer>();

        private readonly List<LobbyPlayer> _players = new();

        #region Unity methods
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

            _settingsPresenter.Init(_settingsConfig);
        }
        #endregion


        #region Player data changing

        public void HandleChangeNameRequest(int index, string newName)
        {
            if (!CanSetName(index, newName))
            {
                return;
            }

            ChangeName(index, newName);
        }

        public void ChangeName(int index, string newName)
        {
            _players[index].Name = newName;
            OnNameChanged?.Invoke(index, newName);
        }

        public void ChangeColor(int index, Color color)
        {
            OnColorChanged?.Invoke(index, color);
        }

        public void ChangeReady(int index, bool isReady)
        {
            OnReadyChanged?.Invoke(index, isReady);

            ReadyStatusChanged();
        }

        public void DisconnectPlayer(int index)
        {
            _players[index].GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
        }

        private string GetDefaultName(int index)
        {
            return $"{_settingsConfig.DefaultNamePrefix}{index}";
        }

        private bool CanSetName(int index, string newName)
        {
            bool isOtherDefault = (newName.StartsWith(_settingsConfig.DefaultNamePrefix) &&
                int.TryParse(newName[_settingsConfig.DefaultNamePrefix.Length..], out int number) &&
                number >= 0 &&
                number < _settingsConfig.MaxPlayers &&
                number != index);

            if (newName.Length < _settingsConfig.NameLengthRange[0]
                || newName.Length > _settingsConfig.NameLengthRange[1]
                || (_players.Any(x => x.Name == newName))
                || isOtherDefault)
            {
                return false;
            }

            return true;
        }

        #endregion


        #region Overrides of NetworkManager

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            if (Utils.IsSceneActive(RoomScene) && _players.Count < _settingsConfig.MaxPlayers)
            {
                ChangeEnoghReady(false);

                var lobbyPlayer = Instantiate(_lobbyPlayerPrefab, Vector3.zero, Quaternion.identity);

                lobbyPlayer.PlayerID = _players.Count;
                lobbyPlayer.Init(GetDefaultName(lobbyPlayer.PlayerID));

                lobbyPlayer.OnNameChangeRequested += HandleChangeNameRequest;
                lobbyPlayer.OnColorChanged += ChangeColor;
                lobbyPlayer.OnReadyChanged += ChangeReady;

                NetworkServer.AddPlayerForConnection(conn, lobbyPlayer.gameObject);

                OnPlayerAdded?.Invoke(lobbyPlayer.PlayerID, lobbyPlayer.isOwned);

                _players.Add(lobbyPlayer);

                foreach (var player in _players)
                {
                    OnSlotUpdated?.Invoke(player.PlayerID, player.Name, player.Color, player.ReadyToBegin, player.isOwned);
                }
            }
            else
            {
                // Late joiners not supported...should've been kicked by OnServerDisconnect
                Debug.Log($"Not in Room scene or players amount exceeded...disconnecting {conn}");
                conn.Disconnect();
            }
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
                    ChangeReady(disconnectedPlayer.PlayerID, false);

                    //disconnectedPlayer.OnNameChanged -= ChangeName;
                    disconnectedPlayer.OnNameChangeRequested -= HandleChangeNameRequest;
                    disconnectedPlayer.OnColorChanged -= ChangeColor;
                    disconnectedPlayer.OnReadyChanged -= ChangeReady;

                    _players.Remove(disconnectedPlayer);

                    for (int i = disconnectedPlayer.PlayerID; i < _players.Count; i++)
                    {
                        var newName = _players[i].Name;

                        if (_players[i].Name == GetDefaultName(i + 1))
                        {
                            newName = GetDefaultName(i);
                            _players[i].Name = newName;
                        }

                        _players[i].PlayerID = i;

                        OnSlotUpdated?.Invoke(_players[i].PlayerID, _players[i].Name, _players[i].Color, _players[i].ReadyToBegin, _players[i].isOwned);
                    }

                    var emptyIndex = _players.Count;

                    OnSlotEmptied?.Invoke(emptyIndex);
                }
            }

            base.OnServerDisconnect(conn);

            // Restart the server if we're headless and no players are connected.
            // This will send server to offline scene, where auto-start will run.
            if (Utils.IsHeadless() && numPlayers < 1)
            {
                StopServer();
            }
        }


        public override void OnServerReady(NetworkConnectionToClient conn)
        {
            base.OnServerReady(conn);

            if (conn != null && conn.identity != null)
            {
                GameObject lobbyPlayer = conn.identity.gameObject;

                if (lobbyPlayer != null && lobbyPlayer.GetComponent<LobbyPlayer>() != null)
                {
                    SceneLoadedForPlayer(conn, lobbyPlayer);
                }
            }
        }

        public override void ServerChangeScene(string newSceneName)
        {
            if (newSceneName == RoomScene)
            {
                foreach (LobbyPlayer lobbyPlayer in _players)
                {
                    if (lobbyPlayer == null)
                        continue;

                    // find the game-player object for this connection, and destroy it
                    NetworkIdentity identity = lobbyPlayer.GetComponent<NetworkIdentity>();

                    if (NetworkServer.active)
                    {
                        // re-add the room object
                        lobbyPlayer.GetComponent<LobbyPlayer>().ReadyToBegin = false;
                        NetworkServer.ReplacePlayerForConnection(identity.connectionToClient, lobbyPlayer.gameObject, ReplacePlayerOptions.KeepAuthority);
                    }
                }

                ChangeEnoghReady(false);
            }

            base.ServerChangeScene(newSceneName);
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            if (sceneName != RoomScene)
            {
                // call SceneLoadedForPlayer on any players that become ready while we were loading the scene.
                foreach (PendingPlayer pending in _pendingPlayers)
                {
                    SceneLoadedForPlayer(pending.conn, pending.roomPlayer);
                }

                _pendingPlayers.Clear();
            }
            else //in case for return to the RoomScene
            {
                if (_settingsPresenter == null)
                {
                    _settingsPresenter = FindAnyObjectByType<LobbyPlayerSettingsPresenter>();
                    _settingsPresenter.Init(_settingsConfig);
                }
            }

                OnRoomServerSceneChanged(sceneName);
        }

        public override void OnStartServer()
        {
            if (string.IsNullOrWhiteSpace(RoomScene))
            {
                Debug.LogError("NetworkRoomManager RoomScene is empty. Set the RoomScene in the inspector for the NetworkRoomManager");
                return;
            }

            if (string.IsNullOrWhiteSpace(GameplayScene))
            {
                Debug.LogError("NetworkRoomManager PlayScene is empty. Set the PlayScene in the inspector for the NetworkRoomManager");
                return;
            }
        }

        public override void OnStopServer()
        {
            _players.Clear();
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

        #endregion


        #region Ready check and handle

        public virtual void ReadyStatusChanged()
        {
            int currentPlayers = 0;
            int readyPlayers = 0;

            foreach (LobbyPlayer player in _players)
            {
                if (player != null)
                {
                    currentPlayers++;
                    if (player.ReadyToBegin)
                        readyPlayers++;
                }
            }

            if (currentPlayers == readyPlayers)
                CheckReadyToBegin();
            else
                ChangeEnoghReady(false);
        }

        public void CheckReadyToBegin()
        {
            if (!Utils.IsSceneActive(RoomScene))
                return;

            int numberOfReadyPlayers = NetworkServer.connections.Count(conn =>
                conn.Value != null &&
                conn.Value.identity != null &&
                conn.Value.identity.TryGetComponent(out LobbyPlayer player) &&
                player.ReadyToBegin);

            bool enoughReadyPlayers = 
                numberOfReadyPlayers >= _settingsConfig.MinPlayers
                && numberOfReadyPlayers <= _settingsConfig.MaxPlayers;

            ChangeEnoghReady(enoughReadyPlayers);

            if (enoughReadyPlayers)
            {
                _pendingPlayers.Clear();
            }
        }

        private void ChangeEnoghReady(bool isEnoghReady)
        {
            if (isEnoghReady)
            {
                if (Utils.IsHeadless())
                {
                    ServerChangeScene(GameplayScene);
                }
            }

            OnCanStartChanged?.Invoke(isEnoghReady);
        }
        #endregion


        #region Scene change

        public void LoadGameScene()
        {
            ServerChangeScene(GameplayScene);
        }

        private void SceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer)
        {
            if (Utils.IsSceneActive(RoomScene))
            {
                Debug.Log("on room scene");

                // cant be ready in room, add to ready list
                PendingPlayer pending;
                pending.conn = conn;
                pending.roomPlayer = roomPlayer;
                _pendingPlayers.Add(pending);
                return;
            }

            Transform startPos = GetStartPosition();

            GameObject gamePlayer = startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

            if (!OnRoomServerSceneLoadedForPlayer(roomPlayer, gamePlayer))
            {
                Debug.Log("not onroomserve...");
                return;
            }

            NetworkServer.ReplacePlayerForConnection(conn, gamePlayer, ReplacePlayerOptions.KeepAuthority);
        }

        public bool OnRoomServerSceneLoadedForPlayer(GameObject roomPlayer, GameObject gamePlayer)
        {
            if (roomPlayer.TryGetComponent<LobbyPlayer>(out var lobbyPlayer))
            {
                OnSlotEmptied?.Invoke(lobbyPlayer.PlayerID);

                if (gamePlayer.TryGetComponent<GamePlayer>(out var player))
                {
                    player.Name = lobbyPlayer.Name;
                    player.Color = lobbyPlayer.Color;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// This is called on the server when a networked scene finishes loading.
        /// </summary>
        /// <param name="sceneName">Name of the new scene.</param>
        private void OnRoomServerSceneChanged(string sceneName) { }

        #endregion
    }
}