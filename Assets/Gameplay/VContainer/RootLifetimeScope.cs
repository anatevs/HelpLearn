using VContainer;
using VContainer.Unity;
using UnityEngine;
using EventBusNamespace;
using Gameplay;
using UI;
using GameManagement;

public class RootLifetimeScope : LifetimeScope
{
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

    [SerializeField]
    private ItemsPrefabsConfig _itemsPrefabsConfig;

    [SerializeField]
    private EnemyPrefabsConfig _enemyPrefabsConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<EventBus>(Lifetime.Singleton);

        RegisterFactories(builder);

        RegisterPrefabServices(builder);
        ImmediateRegisterPlainClasses(builder);
    }

    private void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<SimpleFactory<Item>>(Lifetime.Singleton)
            .WithParameter<Item[]>(_itemsPrefabsConfig.Prefabs)
            .AsImplementedInterfaces();

        builder.Register<SimpleFactory<Enemy>>(Lifetime.Singleton)
            .WithParameter<Enemy[]>(_enemyPrefabsConfig.Prefabs)
            .AsImplementedInterfaces();
    }

    private void RegisterPrefabServices(IContainerBuilder builder)
    {
        ImmediateRegisterMBFromPrefab(_projectileSpawnServicePrefab, builder);
        ImmediateRegisterMBFromPrefab(_enemyServicePrefab, builder);
        ImmediateRegisterMBFromPrefab(_itemServicePrefab, builder);
        ImmediateRegisterMBFromPrefab(_canvasViewPrefab, builder);
        ImmediateRegisterMBFromPrefab(_soundPlayerPrefab, builder);
    }

    private void ImmediateRegisterPlainClasses(IContainerBuilder builder)
    {
        builder.Register<GameStateService>(Lifetime.Singleton)
            .AsImplementedInterfaces().AsSelf();

        builder.Register<PlayerCountersController>(Lifetime.Singleton)
            .AsImplementedInterfaces().AsSelf();

        builder.Register<SaveService>(Lifetime.Singleton)
            .AsImplementedInterfaces().AsSelf();

        builder.Register<GameStateUIController>(Lifetime.Singleton)
            .AsImplementedInterfaces().AsSelf();

        builder.RegisterBuildCallback(resolver =>
        {
            resolver.Resolve<GameStateService>();
            resolver.Resolve<PlayerCountersController>();
            resolver.Resolve<SaveService>();

            resolver.Resolve<GameStateUIController>();
        });
    }

    private void ImmediateRegisterMBFromPrefab<T>(T prefab, IContainerBuilder builder) where T : MonoBehaviour
    {
        GameObject instanceGO = Instantiate(prefab.gameObject);

        T instance = instanceGO.GetComponent<T>();
        instance.name = instance.GetType().Name;

        DontDestroyOnLoad(instanceGO);

        builder.RegisterComponent<T>(instance)
            .AsImplementedInterfaces()
            .AsSelf();
    }
}