using Gameplay;
using GameTest;
using UI;
using UnityEngine;

namespace GameManagement
{
    public class GameInitializer : MonoBehaviour
    {
        [Header("GameplayServices")]
        [SerializeField]
        private MovablesSystem _movablesSystem;

        [SerializeField]
        private LifetimedSystem _lifetimedSystem;

        [Header("Targets")]
        [SerializeField]
        private Transform _targetsTransform;

        [SerializeField]
        private TargetSpawnPoint[] _targetSpawnPoints;

        [SerializeField]
        private TargetConfig _targetConfig;

        [Header("Turret")]
        [SerializeField]
        private Turret _turret;

        [SerializeField]
        private Transform _projectilesTransform;

        [SerializeField]
        private ProjectileTypesConfig _projectileTypesConfig;

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

        [SerializeField]
        private UITitlesConfig _titlesConfig;

        private TargetSpawnService _targetSpawnService;

        private ProjectileSpawnService _projectileService;

        private SpawnCounterService _spawnCounterService;

        private ModePresenter _modePresenter;

        private void Awake()
        {
            _modePresenter = new ModePresenter(_modeView, _spawnModeController);

            var targetPool = _spawnModeController
                .CreatePool<Target>(_targetConfig.Prefab,
                _targetPoolInitCount,
                _targetsTransform);

            _targetSpawnService = new TargetSpawnService(targetPool, _movablesSystem);

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

            _projectileService = new ProjectileSpawnService(_projectileTypesConfig,
                _turret.DamagableMask, _movablesSystem, _lifetimedSystem);

            var projectileTypesData = _projectileTypesConfig.GetData();

            var typeInitCount = (int)Mathf.Ceil(_projectilePoolInitCount / projectileTypesData.Count);

            foreach (var projectileData in projectileTypesData.Values)
            {
                var projectilePool = _spawnModeController
                    .CreatePool<Projectile>(projectileData.Prefab,
                    typeInitCount,
                    _projectilesTransform);

                _projectileService.AddPool(projectileData.Type, projectilePool);
            }

            _turret.Init(_projectileService);

            _spawnCounterService = new SpawnCounterService();
            _spawnInfoController.Init(_spawnCounterService);
            _performancePresenter.Init(_spawnCounterService);

            _spawnCounterService.AddSpawnService(_targetSpawnService, _titlesConfig.TargetsTitle);
            _spawnCounterService.AddSpawnService(_projectileService, _titlesConfig.ProjectilesTitle);
        }

        private void OnDestroy()
        {
            _spawnCounterService.Dispose();
            _modePresenter.Dispose();
        }
    }
}