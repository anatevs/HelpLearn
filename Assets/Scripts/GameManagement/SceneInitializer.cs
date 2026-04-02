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

        private ScoreStorage _scoreStorage;
        private PlayerStatsController _playerStatsController;
        private EndGameController _endGameController;
        private WeaponsPresenterManager _weaponsPresenterManager; 

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
        }

        private void OnDisable()
        {
            _player.OnPlayerKilled -= HandleLoseGame;
            _restartGameController.OnRestartClicked -= ResetLevel;
        }

        public void Init()
        {
            _weaponStorage.Init();
        }

        private void ResetLevel()
        {
            _player.ResetLevel();
            _weaponStorage.ResetLevel();
            _weaponsPresenterManager.ResetLevel();
            _projectileSpawnService.ResetLevel();
            _enemySpawnService.ResetLevel();
            _barrelsSevice.ResetLevel();
            _doorsManager.ResetLevel();
            _scoreStorage.Init(_player.DataConfig.StartScore);

            var playState = new PlayingGameState();
            _gameStatesManager.SetState(playState);
        }

        private void ConstuctServices()
        {
            _player.Construct(_weaponStorage);

            _weaponStorage.Construct(_projectileSpawnService);

            _enemySpawnService.Construct(_player);
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

            _weaponsPresenterManager = new WeaponsPresenterManager(_weaponMenu, _weaponStorage);
        }

        private void HandleLoseGame()
        {
            var endState = new LoseGameState(_endGameController);
            _gameStatesManager.SetState(endState);
        }
    }
}