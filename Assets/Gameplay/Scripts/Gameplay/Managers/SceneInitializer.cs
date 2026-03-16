using EventBusNamespace;
using GameManagement;
using UI;
using UnityEngine;

namespace Gameplay
{
    public sealed class SceneInitializer : MonoBehaviour
    {
        [SerializeField]
        private InstancesService _instancesService;

        [SerializeField]
        private EventBus _eventBus;

        [SerializeField]
        private Player _player;

        [SerializeField]
        private PatrolZones _patrolZones;

        [SerializeField]
        private int _startScore = 0;

        [SerializeField]
        private int _startPickedItems = 0;

        private EnemySpawnService _enemySpawnService;

        private ProjectileSpawnService _projectileSpawnService;

        private ItemsService _itemsService;

        private CanvasView _canvasView;

        private PlayerCountersController _playerCountersController;

        private void OnEnable()
        {
            _eventBus.Subscribe<RestartEvent>(ResetLevel);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<RestartEvent>(ResetLevel);
        }

        private void Start()
        {
            _patrolZones.Init();

            _enemySpawnService = _instancesService.GetInstance<EnemySpawnService>();

            _projectileSpawnService = _instancesService.GetInstance<ProjectileSpawnService>();

            _itemsService = _instancesService.GetInstance<ItemsService>();

            _canvasView = _instancesService.GetInstance<CanvasView>();

            _playerCountersController = _instancesService.GetInstance<PlayerCountersController>();


            _projectileSpawnService.Init();

            _itemsService.Init();


            var scoreStorage = new ScoreStorage(_startScore, _player.Config.WinScore, _eventBus);
            var pickedStorage = new PickedItemsStorage(_startPickedItems);


            _canvasView.Init();

            _playerCountersController.Init(_canvasView, _player, scoreStorage, pickedStorage);

            _eventBus.RaiseEvent(new GamePlayingEvent());


            _player.Construct(_projectileSpawnService);
            _enemySpawnService.Construct(_player, _patrolZones.Locations, _projectileSpawnService);
        }

        public void ResetLevel(RestartEvent e)
        {
            _enemySpawnService.Reset();

            _projectileSpawnService.Reset();

            _itemsService.Reset();

            _playerCountersController.Reset();

            _eventBus.RaiseEvent(new GamePlayingEvent());
        }
    }
}