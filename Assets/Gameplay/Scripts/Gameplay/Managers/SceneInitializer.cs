using EventBusNamespace;
using GameManagement;
using System.Collections.Generic;
using UI;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public sealed class SceneInitializer : MonoBehaviour
    {
        [SerializeField]
        private PatrolZones _patrolZones;

        [SerializeField]
        private int _startScore = 0;

        [SerializeField]
        private int _startPickedItems = 0;

        [SerializeField]
        private EnemySpawnConfig _enemySpawnConfig;

        [SerializeField]
        private ItemServiceConfig _itemsConfig;

        private EventBus _eventBus;

        private Player _player;

        private IEnumerable<IResetable> _resetables;

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
            EventBus eventBus,
            Player player,
            IEnumerable<IResetable> resetables)
        {
            _enemySpawnService = enemySpawnService;
            _projectileSpawnService = projectileSpawnService;
            _itemsService = itemsService;
            _canvasView = canvasView;
            _playerCountersController = playerCountersController;

            _eventBus = eventBus;
            _player = player;

            _resetables = resetables;

            _patrolZones.Init();

            _projectileSpawnService.InitOnScene();

            _itemsService.InitOnScene(_itemsConfig);

            _canvasView.InitOnScene();

            var scoreStorage = new ScoreStorage(_startScore, _player.Config.WinScore, _eventBus);
            var pickedStorage = new PickedItemsStorage(_startPickedItems);

            _playerCountersController.InitOnScene(_player, scoreStorage, pickedStorage);

            _enemySpawnService.InitOnScene(_enemySpawnConfig, _player, _patrolZones.Locations);

            ResetLevel();

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

        public void ResetLevel(RestartEvent e)
        {
            ResetLevel();
        }

        private void ResetLevel()
        {
            foreach (var resetable in _resetables)
            {
                resetable.Reset();
            }

            _eventBus.RaiseEvent(new GamePlayingEvent());
        }
    }
}