using VContainer;
using VContainer.Unity;
using UnityEngine;
using EventBusNamespace;
using Gameplay;
using UI;
using GameManagement;
using AssetsLoading;

public class RootLifetimeScope : LifetimeScope
{
    private AssetsLoader _assetsLoader;
    private SceneSpawnsConfig _sceneSpawnsConfig;

    public void SetLoaderInfo(AssetsLoader assetsLoader, SceneSpawnsConfig sceneSpawnsConfig)
    {
        _assetsLoader = assetsLoader;
        _sceneSpawnsConfig = sceneSpawnsConfig;
    }

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_assetsLoader)
            .AsImplementedInterfaces();

        builder.Register<EventBus>(Lifetime.Singleton);

        RegisterFactories(builder);

        RegisterPrefabServices(builder);

        ImmediateRegisterPlainClasses(builder);

        builder.Register<SceneLoader>(Lifetime.Singleton);
    }

    private void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<EnemyFactory>(container =>
        {
            return new EnemyFactory(container, _sceneSpawnsConfig.EnemySpawnConfig.Configs);
        },
        Lifetime.Singleton)
            .AsImplementedInterfaces();


        builder.Register<ItemsFactory>(container =>
        {
            return new ItemsFactory(container, _sceneSpawnsConfig.ItemSpawnConfig.Configs);
        },
        Lifetime.Singleton)
            .AsImplementedInterfaces();
    }

    private void RegisterPrefabServices(IContainerBuilder builder)
    {
        RegisterService<ProjectileSpawnService>(builder);
        RegisterService<EnemySpawnService>(builder);
        RegisterService<ItemsService>(builder);
        RegisterService<CanvasView>(builder);
        RegisterService<SoundPlayer>(builder);
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

    private void RegisterService<T>(IContainerBuilder builder) where T : MonoBehaviour
    {
        var instanceGO = _assetsLoader.GetServiceGO<T>();

        var instance = instanceGO.GetComponent<T>();

        RegisterDDOL<T>(instance, builder);
    }

    private void RegisterDDOL<T>(T instance, IContainerBuilder builder) where T : MonoBehaviour
    {
        instance.name = instance.GetType().Name;

        DontDestroyOnLoad(instance.gameObject);

        builder.RegisterComponent<T>(instance)
            .AsImplementedInterfaces()
            .AsSelf();
    }
}