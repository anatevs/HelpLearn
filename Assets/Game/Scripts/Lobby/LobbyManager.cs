using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameManagement
{
    public class LobbyManager : NetworkManager
    {
        public event Action<bool> OnCanStartChanged;
        public event Action<string, int> OnServerPlayerDisconnected;
        public event Action OnClientDisconnected;
        public event Action<GamePlayer> OnGamePlayerSpawned;
        public event Action OnServerStopped;
        public event Action OnServerGameplayStarted;
        public event Action OnClientGameplayStarted;

        public GameInitializer GameInitializer => _gameInitializer;

        public LobbyPlayersManager LobbyPlayersManager => _lobbyPlayersManager;

        public MultiplayerSettingsConfig MultiplayerSettingsConfig => _settingsConfig;

        [Header("LobbySettings")]
        [SerializeField]
        [Tooltip("Prefab to use for the Room Player")]
        private LobbyPlayer _lobbyPlayerPrefab;

        [SerializeField]
        private MultiplayerSettingsConfig _settingsConfig;

        [SerializeField]
        private GameInitializer _gameInitializer;

        private SceneStateManager _sceneStateManager;

        private LobbyPlayersManager _lobbyPlayersManager;

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
                    Debug.LogError("LobbyPlayer prefab must have a NetworkIdentity component.");
                }
            }
        }

        public void ConstructOnServer(LobbyPlayersManager lobbyPlayersManager,
            SceneStateManager sceneStateManager)
        {
            _lobbyPlayersManager = lobbyPlayersManager;

            _lobbyPlayersManager.OnReadyChanged += HandleReadyChange;

            _sceneStateManager = sceneStateManager;

            _sceneStateManager.OnSceneLoadRequested += ServerChangeScene;
        }

        public void ConstructOnClient(SceneStateManager sceneStateManager)
        {
            _sceneStateManager = sceneStateManager;
        }

        private void UnsubscribeOnServer()
        {
            if (_lobbyPlayersManager != null)
            {
                _lobbyPlayersManager.OnReadyChanged -= HandleReadyChange;
            }

            if (_sceneStateManager != null)
            {
                _sceneStateManager.OnSceneLoadRequested -= ServerChangeScene;
            }
        }

        #endregion


        #region Player data changing
        public void DisconnectPlayer(int index)
        {
            _lobbyPlayersManager.GetPlayer(index)
                .GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
        }
        #endregion


        #region Overrides of NetworkManager

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            if (_sceneStateManager.IsStartLobby
                && _lobbyPlayersManager.Count < _settingsConfig.MaxPlayers)
            {
                ChangeEnoughReady(false);

                var lobbyPlayer = Instantiate(_lobbyPlayerPrefab, Vector3.zero, Quaternion.identity);

                _lobbyPlayersManager.AddPlayer(lobbyPlayer, conn);
            }
            else
            {
                // Late joiners not supported...should've been kicked by OnServerDisconnect
                Debug.Log($"Server is not on starting lobby scene or players amount exceeded...disconnecting {conn}");
                conn.Disconnect();
            }
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            // cannot join game in progress
            if (!_sceneStateManager.IsInLobby)
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
                var remainPlayers = _lobbyPlayersManager.Count;

                if (conn.identity.TryGetComponent<LobbyPlayer>(out var disconnectedPlayer))
                {
                    _lobbyPlayersManager.RemovePlayer(disconnectedPlayer);

                    playerName = disconnectedPlayer.Name;

                    CheckReadiness();
                }

                else if (conn.identity.TryGetComponent<GamePlayer>(out var gamePlayer))
                {
                    playerName = gamePlayer.Name;
                    _gamePlayers.Remove(gamePlayer);

                    var lobbyInstance = _lobbyPlayersManager.GetPlayer(playerName);

                    if (lobbyInstance != null)
                    {
                        _lobbyPlayersManager.RemovePlayer(lobbyInstance);
                        NetworkServer.Destroy(lobbyInstance.gameObject);
                    }
                }

                remainPlayers = _lobbyPlayersManager.Count;

                OnServerPlayerDisconnected?.Invoke(playerName, remainPlayers);
            }

            base.OnServerDisconnect(conn);

            // Restart the server if we're headless and no players are connected.
            // This will send server to offline scene, where auto-start will run.
            if (Utils.IsHeadless() && numPlayers < 1)
            {
                StopServer();
            }
        }

        public override void OnClientDisconnect()
        {
            base.OnClientDisconnect();

            OnClientDisconnected?.Invoke();
        }


        public override void OnServerReady(NetworkConnectionToClient conn)
        {
            base.OnServerReady(conn);

            if (conn != null && conn.identity != null)
            {
                GameObject lobbyPlayerGO = conn.identity.gameObject;

                if (lobbyPlayerGO != null 
                    && lobbyPlayerGO.GetComponent<LobbyPlayer>() != null
                    && !_sceneStateManager.IsInLobby)
                {
                    ChangeToGamePlayer(conn, lobbyPlayerGO);
                }
            }
        }

        public override void ServerChangeScene(string newSceneName)
        {
            if(_sceneStateManager.IsLobby(newSceneName))
            {
                for (int i = 0; i < _lobbyPlayersManager.Count; i++)
                {
                    var lobbyPlayer = _lobbyPlayersManager.GetPlayer(i);

                    if (lobbyPlayer == null)
                        continue;

                    // find the game-player object for this connection, and destroy it
                    NetworkIdentity identity = lobbyPlayer.GetComponent<NetworkIdentity>();

                    if (NetworkServer.active)
                    {
                        // re-add the room object
                        NetworkServer.ReplacePlayerForConnection(
                            identity.connectionToClient,
                            lobbyPlayer.gameObject, ReplacePlayerOptions.KeepAuthority);
                    }
                }

                ChangeEnoughReady(false);
            }

            base.ServerChangeScene(newSceneName);
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            if (_sceneStateManager.IsLobby(sceneName))
            {
                OnCanStartChanged?.Invoke(true);
            }
            else if (_sceneStateManager.IsGameplay(sceneName))
            {
                OnServerGameplayStarted?.Invoke();
            }
        }

        public override void OnClientSceneChanged()
        {
            base.OnClientSceneChanged();


            if (_sceneStateManager.IsInGameplay)
            {
                OnClientGameplayStarted?.Invoke();
            }
        }

        public override void OnStartServer()
        {
            _gameInitializer.InitOnServer(_settingsConfig, this);
        }

        public override void OnStartClient()
        {
            if (_lobbyPlayerPrefab == null || _lobbyPlayerPrefab.gameObject == null)
                Debug.LogError("NetworkRoomManager no RoomPlayer prefab is registered. Please add a RoomPlayer prefab.");
            else
                NetworkClient.RegisterPrefab(_lobbyPlayerPrefab.gameObject);

            if (playerPrefab == null)
                Debug.LogError("NetworkRoomManager no GamePlayer prefab is registered. Please add a GamePlayer prefab.");

            _gameInitializer.InitOnClient(_settingsConfig, this);
        }

        public override void OnStopServer()
        {
            UnsubscribeOnServer();

            OnServerStopped?.Invoke();
        }

        #endregion


        #region Ready check and handle

        private void HandleReadyChange(int playerId, bool isReady)
        {
            CheckReadiness();
        }

        public void CheckReadiness()
        {
            int currentPlayers = 0;
            int readyPlayers = 0;

            for (int i = 0; i < _lobbyPlayersManager.Count; i++)
            {
                var player = _lobbyPlayersManager.GetPlayer(i);

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
                ChangeEnoughReady(false);
        }

        public void CheckReadyToBegin()
        {
            if (!_sceneStateManager.IsInLobby)
                return;

            int numberOfReadyPlayers = NetworkServer.connections.Count(conn =>
                conn.Value != null &&
                conn.Value.identity != null &&
                conn.Value.identity.TryGetComponent(out LobbyPlayer player) &&
                player.ReadyToBegin);

            bool enoughReadyPlayers = 
                numberOfReadyPlayers >= _settingsConfig.MinPlayers
                && numberOfReadyPlayers <= _settingsConfig.MaxPlayers;

            ChangeEnoughReady(enoughReadyPlayers);
        }

        private void ChangeEnoughReady(bool isEnoghReady)
        {
            if (isEnoghReady)
            {
                if (Utils.IsHeadless())
                {
                    _sceneStateManager.LoadGameScene();
                }
            }

            OnCanStartChanged?.Invoke(isEnoghReady);
        }
        #endregion

        private void ChangeToGamePlayer(NetworkConnectionToClient conn, GameObject lobbyPlayerGO)
        {
            Transform startPos = GetStartPosition();

            GameObject playerGO = startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);


            if (lobbyPlayerGO.TryGetComponent<LobbyPlayer>(out var lobbyPlayer) &&
                playerGO.TryGetComponent<GamePlayer>(out var gamePlayer))
            {
                gamePlayer.Name = lobbyPlayer.Name;
                gamePlayer.Color = lobbyPlayer.Color;
            }
            else
            {
                Debug.Log("player is not LobbyPlayer or try to change to not GamePlayer...");
                return;
            }

            gamePlayer.SetReady();

            NetworkServer.ReplacePlayerForConnection(conn, playerGO, ReplacePlayerOptions.KeepAuthority);

            gamePlayer = playerGO.GetComponent<GamePlayer>();

            _gamePlayers.Add(gamePlayer);

            OnGamePlayerSpawned?.Invoke(gamePlayer);
        }
    }
}