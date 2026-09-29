using Gameplay;
using UI;
using UnityEngine;
using System.Collections.Generic;
using Assets.Input;

namespace GameManagement
{
    public sealed class SceneInitializer : MonoBehaviour
    {
        [SerializeField]
        private InputHandler _input;

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
        private SelectableObjectsManager _selectableObjectsManager;

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

        private PausablesManager _pausablesManager;

        private void Awake()
        {
            ConstuctServices();

            ConstructPlainClasses();

            ConstructControllers();

            Init();

            _pausablesManager = new PausablesManager();
            _pausablesManager.Add(_player);
            _pausablesManager.Add(_selectableObjectsManager);
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

            _wavesManager.OnWaveCompleted += HandleWaveCompleted;
            _wavesManager.OnWaveStarted += HandleWaveStart;
        }

        private void OnDisable()
        {
            _player.OnPlayerKilled -= HandleLoseGame;
            _restartGameController.OnRestartClicked -= ResetLevel;
            _wavesManager.OnAllWavesCompleted -= HandleWinGame;

            _wavesManager.OnWaveCompleted -= HandleWaveCompleted;
            _wavesManager.OnWaveStarted -= HandleWaveStart;
        }

        public void Init()
        {
            _weaponStorage.Init();

            _selectableObjectsManager.Init(_input);
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
            _selectableObjectsManager.ResetLevel();
            _scoreStorage.Init(_player.DataConfig.StartScore);
            _turretManager.ResetLevel();

            _wavesManager.ResetLevel();
            _wavesPanelView.ResetLevel();

            var playState = new PlayingGameState(_pausablesManager);
            _gameStatesManager.SetState(playState);
        }

        private void ConstuctServices()
        {
            _player.Construct(_weaponStorage, _input);

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
            var endState = new LoseGameState(_endGameController, _pausablesManager);
            _gameStatesManager.SetState(endState);
        }

        private void HandleWinGame()
        {
            var endGame = new WinGameState(_endGameController, _pausablesManager);
            _gameStatesManager.SetState(endGame);
        }

        private void HandleWaveCompleted(WaveCompletedInfo waveInfo, bool allEnded)
        {
            _pausablesManager.Pause();
        }

        private void HandleWaveStart()
        {
            _pausablesManager.Resume();
        }
    }
}