using Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private SceneInitializer _sceneInitializer;

    [SerializeField]
    private InputHandler _inputHandler;

    [SerializeField]
    private Player _player;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_inputHandler);

        builder.RegisterComponent(_player)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.RegisterComponent(_sceneInitializer);
    }
}
