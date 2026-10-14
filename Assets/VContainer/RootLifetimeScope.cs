using VContainer;
using VContainer.Unity;
using UnityEngine;
using EventBusNamespace;
using Gameplay;
using UI;
using GameManagement;

public class RootLifetimeScope : LifetimeScope
{
    //[SerializeField]
    //private EventBus _eventBus;

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

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<EventBus>(Lifetime.Singleton);

        RegisterPrefabServices(builder);
        ImmediateRegisterPlainClasses(builder);
    }

    private void RegisterPrefabServices(IContainerBuilder builder)
    {
        ImmediateRegisterMBFromPrefab(_enemyServicePrefab, builder);
        ImmediateRegisterMBFromPrefab(_projectileSpawnServicePrefab, builder);
        ImmediateRegisterMBFromPrefab(_itemServicePrefab, builder);
        ImmediateRegisterMBFromPrefab(_canvasViewPrefab, builder);
        ImmediateRegisterMBFromPrefab(_soundPlayerPrefab, builder);
    }

    private void ImmediateRegisterPlainClasses(IContainerBuilder builder)
    {
        //var gameStates = new GameStateService(_eventBus);

        //builder.RegisterComponent(gameStates);

        //var countersController = new PlayerCountersController(_eventBus);

        //builder.RegisterComponent(countersController);

        //var saveService = new SaveService(_eventBus);

        //builder.RegisterComponent(saveService);


        builder.Register<GameStateService>(Lifetime.Singleton);
        builder.Register<PlayerCountersController>(Lifetime.Singleton);
        builder.Register<SaveService>(Lifetime.Singleton);



        builder.Register<GameStateUIController>(Lifetime.Singleton);
            //.WithParameter(_eventBus);


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

        builder.RegisterComponent<T>(instance);
    }
}