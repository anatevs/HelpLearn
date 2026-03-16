using EventBusNamespace;
using Gameplay;
using UI;
using UnityEngine;

namespace GameManagement
{
    public sealed class BootstrapBuilder : MonoBehaviour
    {
        [SerializeField]
        private InstancesService _instancesService;

        [SerializeField]
        private EventBus _eventBus;

        [SerializeField]
        private EnemySpawnService _enemyServicePrefab;

        [SerializeField]
        private ProjectileSpawnService _projectileSpawnServicePrefab;

        [SerializeField]
        private ItemsService _itemServicePrefab;

        [SerializeField]
        private CanvasView _canvasViewPrefab;

        [SerializeField]
        private SoundPlayer _soundPlayerPrefab;

        private void Awake()
        {
            RegisterPrefabServices();
            RegisterPlainClasses();
        }

        private void RegisterPrefabServices()
        {
            RegisterMBFromPrefab<EnemySpawnService>(_enemyServicePrefab.gameObject);
            RegisterMBFromPrefab<ProjectileSpawnService>(_projectileSpawnServicePrefab.gameObject);
            RegisterMBFromPrefab<ItemsService>(_itemServicePrefab.gameObject);
            RegisterMBFromPrefab<CanvasView>(_canvasViewPrefab.gameObject);
            RegisterMBFromPrefab<SoundPlayer>(_soundPlayerPrefab.gameObject);
        }

        private void RegisterPlainClasses()
        {
            if (!CheckRegistered<GameStateService>())
            {
                var gameStates = new GameStateService(_eventBus);
                RegisterClass(gameStates);
            }

            if (!CheckRegistered<PlayerCountersController>())
            {
                var countersController = new PlayerCountersController(_eventBus);
                RegisterClass(countersController);
            }

            if (!CheckRegistered<SaveService>())
            {
                var saveService = new SaveService(_eventBus);
                RegisterClass(saveService);
            }

            if (!CheckRegistered<GameStateUIController>())
            {
                var gameStateUI = new GameStateUIController(_eventBus, _instancesService.GetInstance<CanvasView>());
                RegisterClass(gameStateUI);
            }
        }

        private void RegisterMBFromPrefab<T>(GameObject prefab) where T : MonoBehaviour
        {
            if (!CheckRegistered<T>())
            {
                var onScene = FindObjectsByType<T>(FindObjectsSortMode.None);
                if (onScene != null && onScene.Length > 0)
                {
                    foreach (var obj in onScene)
                    {
                        Destroy(obj.gameObject);
                    }
                }

                GameObject instanceGO = Instantiate(prefab);

                T instance = instanceGO.GetComponent<T>();
                instance.name = instance.GetType().Name;

                DontDestroyOnLoad(instanceGO);

                _instancesService.AddInstanceType(instance);
            }
        }

        private void RegisterClass<T>(T instance)
        {
            if (!CheckRegistered<T>())
            {
                _instancesService.AddInstanceType<T>(instance);
            }
        }

        private bool CheckRegistered<T>()
        {
            if (_instancesService.ContainsType(typeof(T)))
            {
                Debug.LogWarning($"there is also registered type {typeof(T)} in DI");
                return true;
            }

            return false;
        }
    }
}