using EventBusNamespace;
using GameManagement;
using Gameplay;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private SceneInitializer _sceneInitializer;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_sceneInitializer);
    }
}
