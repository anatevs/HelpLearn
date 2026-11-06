using AssetsLoading;
using Gameplay;
using System;
using System.Threading.Tasks;
using UI;
using UnityEngine;
using VContainer;

namespace GameManagement
{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField]
        private ServiceAssetsConfig _serviceAssets;

        [SerializeField]
        private SceneSpawnsConfig _spawnsConfig;

        [SerializeField]
        private RootLifetimeScope _rootScopePrefab;

        private AssetsLoader _assetsLoader;

        private RootLifetimeScope _rootScope;

        private void Awake()
        {
            _assetsLoader = new AssetsLoader(_serviceAssets);
        }

        private async void Start()
        {
            await LoadServicesInstances();

            await LoadLevelPrefabs();

            _rootScope = Instantiate(_rootScopePrefab);

            DontDestroyOnLoad(_rootScope.gameObject);

            _rootScope.SetLoaderInfo(_assetsLoader, _spawnsConfig);

            _rootScope.Build();

            var sceneLoader = _rootScope.Container.Resolve<SceneLoader>();

            sceneLoader.LoadGameScene();
        }

        private async Task LoadServicesInstances()
        {
            await _assetsLoader.LoadServiceGO<ProjectileSpawnService>();
            await _assetsLoader.LoadServiceGO<EnemySpawnService>();
            await _assetsLoader.LoadServiceGO<ItemsService>();
            await _assetsLoader.LoadServiceGO<CanvasView>();
            await _assetsLoader.LoadServiceGO<SoundPlayer>();
        }

        private async Task LoadLevelPrefabs()
        {
            var enemyConfigs = _spawnsConfig.EnemySpawnConfig.Configs;

            for (int i = 0; i < enemyConfigs.Length; i++)
            {
                await LoadLevelConfigPrefab(enemyConfigs[i]);
            }

            var itemConfigs = _spawnsConfig.ItemSpawnConfig.Configs;

            for (int i = 0; i < itemConfigs.Length; i++)
            {
                await LoadLevelConfigPrefab(itemConfigs[i]);
            }

            await LoadLevelConfigPrefab(_spawnsConfig.ProjectileSpawnConfig);
        }

        private async Task LoadLevelConfigPrefab<T>(IPrefabConfig<T> config) where T : MonoBehaviour
        {
            var prefab = await _assetsLoader.LoadLevelGO(config.PrefabReference);

            if (!prefab.TryGetComponent<T>(out var component))
            {
                throw new Exception($"no component of type {typeof(T)} on prefab {prefab.name}");
            }

            config.Prefab = component;
        }
    }
}