using Gameplay;
using Mirror;
using TMPro;
using UI;
using UnityEngine;

namespace GameManagement
{
    public class GameInitializer : MonoBehaviour
    {
        public LeaderboardStorage LeaderboardStorage => _leaderboardStorage;

        public PlayerSceneDependencies PlayerSceneDependencies => _playerSceneDependencies;

        public SceneStateManager SceneStateManager => _sceneStateManager;

        [SerializeField]
        private GameInfoPanel _infoPanelPrefab;

        [SerializeField]
        private GameLogView _logView;

        [SerializeField]
        private TMP_Text _playersCountText;

        [SerializeField]
        private MatchConfig _matchConfig;

        [SerializeField]
        private SceneConfig _sceneConfig;

        private SceneStateManager _sceneStateManager;

        private GameInfoPanel _gameInfoPanel;

        private LobbyPlayersManager _lobbyPlayersManager;

        private LobbyManager _lobbyManager;

        private LobbyInfoPresenter _lobbyInfoPresenter;

        private LeaderboardStorage _leaderboardStorage;
        private LeaderboardController _leaderboardController;

        private PlayerSceneDependencies _playerSceneDependencies;

        private void Awake()
        {
            _sceneStateManager = new SceneStateManager(_sceneConfig);
        }

        private void OnDestroy()
        {
            _leaderboardController?.Dispose();
        }

        public void InitOnServer(MultiplayerSettingsConfig settingsConfig, LobbyManager lobbyManager)
        {
            SpawnInfoPanel();

            _lobbyManager = lobbyManager;

            _lobbyPlayersManager = new LobbyPlayersManager(settingsConfig);

            _lobbyManager.ConstructOnServer(_lobbyPlayersManager, _sceneStateManager);

            _lobbyInfoPresenter = new LobbyInfoPresenter(_lobbyManager, _lobbyPlayersManager, _gameInfoPanel);

            _leaderboardStorage = new LeaderboardStorage(_matchConfig.KillToScoreCoef);
            _leaderboardController = new LeaderboardController(_leaderboardStorage);


            _lobbyManager.OnGamePlayerSpawned += HandleServerSpawnPlayer;
            _lobbyManager.OnServerGameplayStarted += HandleServerStartGameplay;
            _lobbyManager.OnServerStopped += HandleStopServer;
        }

        public void InitOnClient(MultiplayerSettingsConfig settingsConfig, LobbyManager lobbyManager)
        {
            _lobbyManager = lobbyManager;

            _lobbyManager.OnClientGameplayStarted += HandleClientStartGameplay;

            _lobbyManager.OnClientDisconnected += HandleClientDisconnect;

            _lobbyManager.ConstructOnClient(_sceneStateManager);

            _sceneStateManager.IsStartLobby = true;
        }

        private void SpawnInfoPanel()
        {
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

        public void RegisterInfoPanel(GameInfoPanel gameInfoPanel)
        {
            _gameInfoPanel = gameInfoPanel;
            gameInfoPanel.Init(_logView, _playersCountText);
        }

        private void HandleStopServer()
        {
            if (_lobbyManager != null)
            {
                _lobbyManager.OnGamePlayerSpawned -= HandleServerSpawnPlayer;
                _lobbyManager.OnServerGameplayStarted -= HandleServerStartGameplay;
                _lobbyManager.OnServerStopped -= HandleStopServer;
            }

            _lobbyPlayersManager.Clear();
            _leaderboardController.Clear();
            _lobbyInfoPresenter.Dispose();
        }

        private void HandleClientDisconnect()
        {
            _lobbyManager.OnClientGameplayStarted -= HandleClientStartGameplay;
            _lobbyManager.OnClientDisconnected -= HandleClientDisconnect;
            _lobbyManager = null;

            _sceneStateManager.IsStartLobby = true;
        }

        private void HandleServerStartGameplay()
        {
            _leaderboardController.Clear();

            GetGameplayDependencies();

            _playerSceneDependencies.Construct(_gameInfoPanel, _sceneStateManager);
        }

        private void HandleClientStartGameplay()
        {
            GetGameplayDependencies();

            _sceneStateManager.IsStartLobby = false;
        }

        private void HandleServerSpawnPlayer(GamePlayer player)
        {
            _leaderboardController.AddPlayer(player);
        }

        public void GetGameplayDependencies()
        {
            var sceneObjects = FindAnyObjectByType<PlayerSceneDependencies>();

            if (sceneObjects == null)
            {
                Debug.Log("no PlayerSceneDependencies object on a scene in OnClientSceneChanged()");
                return;
            }

            _playerSceneDependencies = sceneObjects;
        }
    }
}