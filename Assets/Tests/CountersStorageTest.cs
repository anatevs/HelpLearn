using UnityEngine;
using EventBusNamespace;
using Gameplay;
using NUnit.Framework;
using VContainer;
using UI;
using UnityEditor;
using TMPro;


[TestFixture]
public class CountersStorageTest
{
    private EventBus _eventBus;

    private IObjectResolver _resolver;

    private int _startCount = 0;
    private int _winScore = 10;
    private int _scoreChangeValue = 2;

    private Item _mockItem;
    private CounterView _mockView;
    private TMP_Text _mockText;

    [SetUp]
    public void SetUp()
    {
        SetupMockItem();

        SetupMockView();

        var builder = new ContainerBuilder();
        _eventBus = new EventBus();

        builder.RegisterInstance(_eventBus);

        builder.Register<CounterStorage, PickedItemsStorage>(Lifetime.Singleton)
            .WithParameter<int>(_startCount)
            .As<CounterStorage>();

        builder.Register<CounterController<ItemPickedEvent>>(Lifetime.Singleton)
            .WithParameter<CounterView>(_mockView);


        builder.Register<ScoreStorage>(Lifetime.Singleton)
            .WithParameter<int>(_startCount)
            .WithParameter<int>(_winScore);

        builder.Register<ScoreController>(Lifetime.Singleton)
            .WithParameter<CounterView>(_mockView);

        _resolver = builder.Build();
    }

    [Test]
    public void ItemStorage_IncrementsStorageBy1_OnItemPicked()
    {
        _eventBus = _resolver.Resolve<EventBus>();

        var itemsStorage = _resolver.Resolve<CounterStorage>();

        _resolver.Resolve<CounterController<ItemPickedEvent>>();

        _eventBus.RaiseEvent(new ItemPickedEvent(_mockItem));

        Assert.AreEqual(itemsStorage.Value, _startCount + 1);
    }

    [Test]
    public void ScoreStorage_IncrementScore_OnScoreChangeEvent()
    {
        var scoreStorage = _resolver.Resolve<ScoreStorage>();

        _resolver.Resolve<ScoreController>();

        _eventBus.RaiseEvent(new ScoreChangedEvent(_scoreChangeValue));

        Assert.AreEqual(scoreStorage.Value, _startCount + _scoreChangeValue);
    }

    [TearDown]
    public void TearDown()
    {
        _resolver.Dispose();
        GameObject.DestroyImmediate(_mockItem.gameObject);
        GameObject.DestroyImmediate(_mockView.gameObject);
    }

    private void SetupMockItem()
    {
        var go = new GameObject("MockItem");
        go.AddComponent<BoxCollider2D>();
        _mockItem = go.AddComponent<Item>();


        var serializedItem = new SerializedObject(_mockItem);

        SerializedProperty configProperty = serializedItem.FindProperty("_config");

        ItemConfig mockConfig = ScriptableObject.CreateInstance<ItemConfig>();
        configProperty.objectReferenceValue = mockConfig;

        serializedItem.ApplyModifiedProperties();
    }

    private void SetupMockView()
    {
        var goView = new GameObject("PickedCountView");
        _mockView = goView.AddComponent<CounterView>();


        var serializedView = new SerializedObject(_mockView);

        SerializedProperty textProperty = serializedView.FindProperty("_conterText");

        _mockText = goView.AddComponent<TextMeshProUGUI>();
        textProperty.objectReferenceValue = _mockText;

        serializedView.ApplyModifiedProperties();
    }
}