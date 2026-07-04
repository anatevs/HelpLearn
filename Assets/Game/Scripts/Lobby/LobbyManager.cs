using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using Network.UI;
using static Mirror.NetworkRoomManager;
using System.Linq;
using UI;
using Gameplay;

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

        public GameInfoPanel GameInfoPanel => _gameInfoPanel;

        [SerializeField]
        private LobbyHUDPresenter _lobbyHudPresenter;

        [SerializeField]
        private LobbyPlayerSettingsPresenter _settingsPresenter;

        [SerializeField]
        private MultiplayerSettingsConfig _settingsConfig;

        [SerializeField]
        [Tooltip("Prefab to use for the Room Player")]
        private LobbyPlayer _lobbyPlayerPrefab;

        [SerializeField]
        private GameInfoPanel _infoPanelPrefab;

        [SerializeField]
        GameInfoViewInitializer _gameInfoView;

        private GameInfoPanel _gameInfoPanel;

        private PlayerSceneDependencies _playerSceneDependencies;

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

        private readonly List<LobbyPlayer> _lobbyPlayers = new();

        private readonly List<GamePlayer> _gamePlayers = new();

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

        public void RegisterInfoPanel(GameInfoPanel panel)
        {
            _gameInfoPanel = panel;
            _gameInfoView.SetupInfoPanel(_gameInfoPanel);
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
            _lobbyPlayers[index].Name = newName;
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
            _lobbyPlayers[index].GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
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
                || (_lobbyPlayers.Any(x => x.Name == newName))
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
            if (Utils.IsSceneActive(RoomScene) && _lobbyPlayers.Count < _settingsConfig.MaxPlayers)
            {
                ChangeEnoghReady(false);

                var lobbyPlayer = Instantiate(_lobbyPlayerPrefab, Vector3.zero, Quaternion.identity);

                lobbyPlayer.PlayerID = _lobbyPlayers.Count;
                lobbyPlayer.Init(GetDefaultName(lobbyPlayer.PlayerID));

                lobbyPlayer.OnNameChangeRequested += HandleChangeNameRequest;
                lobbyPlayer.OnColorChanged += ChangeColor;
                lobbyPlayer.OnReadyChanged += ChangeReady;

                NetworkServer.AddPlayerForConnection(conn, lobbyPlayer.gameObject);

                OnPlayerAdded?.Invoke(lobbyPlayer.PlayerID, lobbyPlayer.isOwned);

                _lobbyPlayers.Add(lobbyPlayer);

                foreach (var player in _lobbyPlayers)
                {
                    OnSlotUpdated?.Invoke(player.PlayerID, player.Name, player.Color, player.ReadyToBegin, player.isOwned);
                }

                _gameInfoPanel.AddLog($"Connected player: {lobbyPlayer.Name}");
                _gameInfoPanel.SetPlayersCount(_lobbyPlayers.Count);
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
                var playerName = "";
                var remainPlayers = _lobbyPlayers.Count;

                if (conn.identity.TryGetComponent<LobbyPlayer>(out var disconnectedPlayer))
                {
                    ChangeReady(disconnectedPlayer.PlayerID, false);

                    disconnectedPlayer.OnNameChangeRequested -= HandleChangeNameRequest;
                    disconnectedPlayer.OnColorChanged -= ChangeColor;
                    disconnectedPlayer.OnReadyChanged -= ChangeReady;

                    _lobbyPlayers.Remove(disconnectedPlayer);

                    for (int i = disconnectedPlayer.PlayerID; i < _lobbyPlayers.Count; i++)
                    {
                        var newName = _lobbyPlayers[i].Name;

                        if (_lobbyPlayers[i].Name == GetDefaultName(i + 1))
                        {
                            newName = GetDefaultName(i);
                            _lobbyPlayers[i].Name = newName;
                        }

                        _lobbyPlayers[i].PlayerID = i;

                        OnSlotUpdated?.Invoke(_lobbyPlayers[i].PlayerID, _lobbyPlayers[i].Name, _lobbyPlayers[i].Color, _lobbyPlayers[i].ReadyToBegin, _lobbyPlayers[i].isOwned);
                    }

                    var emptyIndex = _lobbyPlayers.Count;

                    OnSlotEmptied?.Invoke(emptyIndex);

                    playerName = disconnectedPlayer.Name;
                    remainPlayers = _lobbyPlayers.Count;
                }

                else if (conn.identity.TryGetComponent<GamePlayer>(out var gamePlayer))
                {
                    playerName = gamePlayer.Name;
                    _gamePlayers.Remove(gamePlayer);

                    remainPlayers = _gamePlayers.Count;
                }

                _gameInfoPanel.AddLog($"Disconnected player: {playerName}");
                _gameInfoPanel.SetPlayersCount(remainPlayers);
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
                foreach (LobbyPlayer lobbyPlayer in _lobbyPlayers)
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

                if (sceneName == GameplayScene)
                {
                    _gameInfoPanel.AddLog($"Match started!");
                }
            }
            else //in case for return to the RoomScene
            {
                if (_settingsPresenter == null)
                {
                    _settingsPresenter = FindAnyObjectByType<LobbyPlayerSettingsPresenter>();
                    _settingsPresenter.Init(_settingsConfig);
                }
            }
        }

        public override void OnClientSceneChanged()
        {
            base.OnClientSceneChanged();

            if (Utils.IsSceneActive(GameplayScene))
            {
                var sceneObjects = FindAnyObjectByType<PlayerSceneDependencies>(); //to Manager as public property with event of init and initing in OnServerSceneChanged or make in dpnds Awake() with injecting itself to mngr

                if (sceneObjects == null)
                {
                    Debug.Log("no PlayerSceneDependencies object on a scene in OnClientSceneChanged()");
                    return;
                }

                _playerSceneDependencies = sceneObjects;
            }
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

            if (_infoPanelPrefab == null)
            {
                Debug.LogError("InfoPanel prefab is null");
                return;
            }
            else
            {
                var infoPanel = Instantiate(_infoPanelPrefab);

                NetworkServer.Spawn(infoPanel.gameObject);

                RegisterInfoPanel(infoPanel);
            }
        }

        public override void OnStopServer()
        {
            _lobbyPlayers.Clear();
        }

        public override void OnStartClient()
        {
            if (_lobbyPlayerPrefab == null || _lobbyPlayerPrefab.gameObject == null)
                Debug.LogError("NetworkRoomManager no RoomPlayer prefab is registered. Please add a RoomPlayer prefab.");
            else
                NetworkClient.RegisterPrefab(_lobbyPlayerPrefab.gameObject);

            if (playerPrefab == null)
                Debug.LogError("NetworkRoomManager no GamePlayer prefab is registered. Please add a GamePlayer prefab.");

            NetworkClient.RegisterPrefab(_infoPanelPrefab.gameObject);
        }

        #endregion


        #region Ready check and handle

        public virtual void ReadyStatusChanged()
        {
            int currentPlayers = 0;
            int readyPlayers = 0;

            foreach (LobbyPlayer player in _lobbyPlayers)
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

            _gamePlayers.Add(gamePlayer.GetComponent<GamePlayer>());
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

                _gameInfoPanel.AddLog($"Spawned player: {player.Name}");

                return true;
            }

            return false;
        }

        public void RegisterGamePlayer(GamePlayer gamePlayer)
        {
            if (_playerSceneDependencies != null)
            {
                _playerSceneDependencies.ConstructPlayer(gamePlayer);
            }
        }

        #endregion
    }
}