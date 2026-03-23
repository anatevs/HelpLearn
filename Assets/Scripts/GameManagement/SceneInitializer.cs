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

        private ScoreStorage _scoreStorage;
        private PlayerStatsController _playerStatsController;

        private void Start()
        {
            Constuct();

            _scoreStorage = new(_enemySpawnService);

            _playerStatsController = 
                new PlayerStatsController(_player.HP, _scoreStorage, _playerStatsView);

            _player.Init();

            _projectileSpawnService.Init();

            _scoreStorage.Init(_player.DataConfig.StartScore);
        }

        private void Constuct()
        {
            _player.Construct(_projectileSpawnService);

            _enemySpawnService.Construct(_player);
        }
    }
}