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
        private EndGameView _endGameView;

        [SerializeField]
        private RestartGameController _restartGameController;

        private ScoreStorage _scoreStorage;
        private PlayerStatsController _playerStatsController;
        private EndGameController _endGameController;

        private void Awake()
        {
            Constuct();

            _scoreStorage = new(_enemySpawnService);

            _playerStatsController = 
                new PlayerStatsController(_player.HP, _scoreStorage, _playerStatsView);

            _endGameController = new EndGameController(_endGameView);
        }

        private void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            _player.OnPlayerKilled += HandleLoseGame;
            _restartGameController.OnRestartClicked += Init;
        }

        private void OnDisable()
        {
            _player.OnPlayerKilled -= HandleLoseGame;
            _restartGameController.OnRestartClicked -= Init;
        }

        public void Init()
        {
            _player.Init();
            _projectileSpawnService.Init();
            _enemySpawnService.Init();
            _scoreStorage.Init(_player.DataConfig.StartScore);

            _endGameController.Hide();
            Time.timeScale = 1;
        }

        private void Constuct()
        {
            _player.Construct(_projectileSpawnService);

            _enemySpawnService.Construct(_player);

            _projectileSpawnService.Construct();
        }

        private void HandleLoseGame()
        {
            _endGameController.ShowLose();

            Time.timeScale = 0;
        }
    }
}