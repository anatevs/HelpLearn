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
        private TargetSpawnService _targetSpawnService;

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

        [SerializeField]
        private ProjectileTypesConfig _projectileTypeSpeedConfig;

        [Header("Pools")]
        [SerializeField]
        private int _targetPoolInitCount;

        [SerializeField]
        private int _projectilePoolInitCount;


        [Header("PerformanceTest")]
        [SerializeField]
        private SpawnModeController _spawnModeController;

        [SerializeField]
        private GameSpawnInfoPresenter _spawnInfoController;

        [SerializeField]
        private TargetSpawnAdjuster _targetSpawnAdjuster;

        [SerializeField]
        private PerformancePresenter _performancePresenter;

        [SerializeField]
        private ModeView _modeView;

        private SpawnCounterService _spawnCounterService;

        private ModePresenter _modePresenter;

        private void Awake()
        {
            _modePresenter = new ModePresenter(_modeView, _spawnModeController);

            _targetSpawnAdjuster.SetupSpawn(_targetSpawnService, _turret);

            if (!_targetSpawnAdjuster.IsTargetsOn)
            {
                foreach (var point in _targetSpawnPoints)
                {
                    point.Init(_targetSpawnService);
                }
            }
            else
            {
                _targetPoolInitCount = _targetSpawnAdjuster.TargetsAmount;
            }

            if (_targetSpawnAdjuster.IsProjectilesOn)
            {
                _projectilePoolInitCount = _targetSpawnAdjuster.ProjectilesInitCount;
            }

            var targetPool = _spawnModeController
                .CreatePool<Target>(_targetConfig.Prefab,
                _targetPoolInitCount,
                _targetSpawnService.PoolTransform);

            _targetSpawnService.InitPool(targetPool);


            _projectileService.Init(_projectileTypeSpeedConfig);

            var projectileTypesData = _projectileTypeSpeedConfig.GetData();

            var typeInitCount = (int)Mathf.Ceil(_projectilePoolInitCount / projectileTypesData.Count);

            foreach (var projectileData in projectileTypesData.Values)
            {
                var projectilePool = _spawnModeController
                    .CreatePool<Projectile>(projectileData.Prefab,
                    typeInitCount,
                    _projectileService.PoolTransform);

                _projectileService.InitPool(projectileData.Type, projectilePool);
            }

            _turret.Init(_projectileService);

            _spawnCounterService = new SpawnCounterService();
            _spawnInfoController.Init(_spawnCounterService);
            _performancePresenter.Init(_spawnCounterService);

            _spawnCounterService.AddSpawnService(_targetSpawnService);
            _spawnCounterService.AddSpawnService(_projectileService);
        }

        private void OnDestroy()
        {
            _spawnCounterService.Dispose();
            _modePresenter.Dispose();
        }
    }
}