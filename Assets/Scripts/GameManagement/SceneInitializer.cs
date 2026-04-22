using Gameplay;
using UI;
using UnityEngine;

namespace GameManagement
{
    public sealed class SceneInitializer : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        [SerializeField]
        private PlayerStatsView _playerStatsView;

        [SerializeField]
        private ProjectileSpawnService _projectileSpawnService;

        [SerializeField]
        private EnemySpawnService _enemySpawnService;

        [SerializeField]
        private WeaponStorage _weaponStorage;

        [SerializeField]
        private BarrelsSevice _barrelsSevice;

        [SerializeField]
        private DoorsManager _doorsManager;

        [SerializeField]
        private EndGameView _endGameView;

        [SerializeField]
        private RestartGameController _restartGameController;

        [SerializeField]
        private WeaponsMenu _weaponMenu;

        [SerializeField]
        private WavesManager _wavesManager;

        [SerializeField]
        private WavesPanelView _wavesPanelView;

        [SerializeField]
        private TurretManager _turretManager;

        [SerializeField]
        private MinimapController _minimapController;

        private ScoreStorage _scoreStorage;
        private PlayerStatsController _playerStatsController;
        private EndGameController _endGameController;
        private WeaponsPanelPresenter _weaponsPanelManager; 
        private WavesPanelController _wavesPanelController;

        private GameStatesManager _gameStatesManager;

        private void Awake()
        {
            ConstuctServices();

            ConstructPlainClasses();

            ConstructControllers();

            Init();
        }

        private void Start()
        {
            ResetLevel();
        }

        private void OnEnable()
        {
            _player.OnPlayerKilled += HandleLoseGame;
            _restartGameController.OnRestartClicked += ResetLevel;
            _wavesManager.OnAllWavesCompleted += HandleWinGame;
        }

        private void OnDisable()
        {
            _player.OnPlayerKilled -= HandleLoseGame;
            _restartGameController.OnRestartClicked -= ResetLevel;
            _wavesManager.OnAllWavesCompleted -= HandleWinGame;
        }

        public void Init()
        {
            _weaponStorage.Init();
        }

        private void ResetLevel()
        {
            _player.ResetLevel();
            _weaponStorage.ResetLevel();
            _weaponsPanelManager.ResetLevel();
            _projectileSpawnService.ResetLevel();
            _minimapController.ResetLevel();
            _enemySpawnService.ResetLevel();
            _barrelsSevice.ResetLevel();
            _doorsManager.ResetLevel();
            _scoreStorage.Init(_player.DataConfig.StartScore);
            _turretManager.ResetLevel();

            _wavesManager.ResetLevel();
            _wavesPanelView.ResetLevel();

            var playState = new PlayingGameState();
            _gameStatesManager.SetState(playState);
        }

        private void ConstuctServices()
        {
            _player.Construct(_weaponStorage);

            _weaponStorage.Construct(_projectileSpawnService);

            _enemySpawnService.Construct(_player);

            _wavesManager.Constuct(_enemySpawnService);

            _turretManager.Construct(_player, _projectileSpawnService);

            _minimapController.Construct(_enemySpawnService);
        }

        private void ConstructPlainClasses()
        {
            _scoreStorage = new(_enemySpawnService);

            _gameStatesManager = new GameStatesManager();
        }

        private void ConstructControllers()
        {
            _playerStatsController =
                new PlayerStatsController(_player.HP, _scoreStorage, _playerStatsView);

            _endGameController = new EndGameController(_endGameView);

            _weaponsPanelManager = new WeaponsPanelPresenter(_weaponMenu, _weaponStorage);

            _wavesPanelController = new WavesPanelController(_wavesManager, _wavesPanelView);
        }

        private void HandleLoseGame()
        {
            var endState = new LoseGameState(_endGameController);
            _gameStatesManager.SetState(endState);
        }

        private void HandleWinGame()
        {
            var endGame = new WinGameState(_endGameController);
            _gameStatesManager.SetState(endGame);
        }
    }
}