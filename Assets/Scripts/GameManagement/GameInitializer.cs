using Gameplay;
using GameTest;
using UI;
using UnityEngine;

namespace GameManagement
{
    public class GameInitializer : MonoBehaviour
    {
        [Header("Targets")]
        [SerializeField]
        private TargetSpawnService _targetService;

        [SerializeField]
        private TargetSpawnPoint[] _targetSpawnPoints;

        [SerializeField]
        private TargetConfig _targetConfig;

        [Header("Turret")]
        [SerializeField]
        private Turret _turret;

        [SerializeField]
        private ProjectileSpawnService _projectileService;

        [SerializeField]
        private ProjectileConfig _projectileConfig;

        [Header("Management")]
        [SerializeField]
        private SpawnModeController _spawnModeController;

        [SerializeField]
        private int _poolsInitCount;

        [SerializeField]
        private GameSpawnInfoController _spawnInfoController;

        private SpawnCounterService _gameSpawnCounter;

        private void Awake()
        {
            var targetPool = _spawnModeController
                .CreatePool<Target>(_targetConfig.Prefab, _poolsInitCount);

            var projectilePool = _spawnModeController
                .CreatePool<Projectile>(_projectileConfig.Prefab, _poolsInitCount);

            _targetService.Init(targetPool);
            foreach (var point in _targetSpawnPoints)
            {
                point.Init(_targetService);
            }

            _projectileService.Init(projectilePool);
            _turret.Init(_projectileService);

            _gameSpawnCounter = new SpawnCounterService();
            _spawnInfoController.Init(_gameSpawnCounter);

            _gameSpawnCounter.AddSpawnService(_projectileService);
            _gameSpawnCounter.AddSpawnService(_targetService);
        }
    }
}