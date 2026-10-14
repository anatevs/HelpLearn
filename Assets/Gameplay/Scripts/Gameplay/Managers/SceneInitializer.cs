using EventBusNamespace;
using UI;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public sealed class SceneInitializer : MonoBehaviour
    {
        //[SerializeField]
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

        [Inject]
        public void Construct(EnemySpawnService enemySpawnService,
            ProjectileSpawnService projectileSpawnService,
            ItemsService itemsService,
            CanvasView canvasView,
            PlayerCountersController playerCountersController,
            EventBus eventBus)
        {
            _enemySpawnService = enemySpawnService;
            _projectileSpawnService = projectileSpawnService;
            _itemsService = itemsService;
            _canvasView = canvasView;
            _playerCountersController = playerCountersController;

            _eventBus = eventBus;


            _patrolZones.Init();

            _projectileSpawnService.Init();

            _itemsService.Init();


            var scoreStorage = new ScoreStorage(_startScore, _player.Config.WinScore, _eventBus);
            var pickedStorage = new PickedItemsStorage(_startPickedItems);


            _canvasView.Init();

            _playerCountersController.Init(_canvasView, _player, scoreStorage, pickedStorage);

            _eventBus.RaiseEvent(new GamePlayingEvent());

            _player.Construct(_projectileSpawnService, _eventBus);
            _enemySpawnService.ConstructInit(_player, _patrolZones.Locations, _projectileSpawnService);



            _eventBus.Subscribe<RestartEvent>(ResetLevel);
        }


        private void OnEnable()
        {
            if (_eventBus != null)
            {
                _eventBus.Subscribe<RestartEvent>(ResetLevel);
            }
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<RestartEvent>(ResetLevel);
        }


        //private void Start()
        //{
        //    _patrolZones.Init();

        //    _projectileSpawnService.Init();

        //    _itemsService.Init();


        //    var scoreStorage = new ScoreStorage(_startScore, _player.Config.WinScore, _eventBus);
        //    var pickedStorage = new PickedItemsStorage(_startPickedItems);


        //    _canvasView.Init();

        //    _playerCountersController.Init(_canvasView, _player, scoreStorage, pickedStorage);

        //    _eventBus.RaiseEvent(new GamePlayingEvent());

        //    _player.Construct(_projectileSpawnService, _eventBus);
        //    _enemySpawnService.ConstructInit(_player, _patrolZones.Locations, _projectileSpawnService);
        //}

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