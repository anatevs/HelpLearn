using EventBusNamespace;
using System;
using UI;
using GameManagement;

namespace Gameplay
{
    public sealed class PlayerCountersController : 
        IResetable,
        IDisposable
    {
        private readonly EventBus _eventBus;

        private readonly CanvasView _canvasView;

        private Player _player;

        private ScoreController _scoreController;

        private CounterController<ItemPickedEvent> _pickedController;

        public PlayerCountersController(EventBus eventBus,
            CanvasView canvasView)
        {
            _eventBus = eventBus;
            _canvasView = canvasView;

            _eventBus.Subscribe<PlayerDamageEvent>(HandlePlayerDamage);
            _eventBus.Subscribe<EnemyKilledEvent>(HandleEnemyKill);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<PlayerDamageEvent>(HandlePlayerDamage);
            _eventBus.Unsubscribe<EnemyKilledEvent>(HandleEnemyKill);
        }

        public void InitOnScene(Player player,
            ScoreStorage scoreStorage,
            PickedItemsStorage pickedItemsStorage)
        {
            _player = player;

            _scoreController = new(scoreStorage, _canvasView.ScoreView, _eventBus);

            _pickedController = new(pickedItemsStorage, _canvasView.PickedItemsView, _eventBus);
        }

        public void Reset()
        {
            _scoreController.Reset();
            _pickedController.Reset();

            _canvasView.HPView.SetCountText(_player.Config.HP.ToString());
        }

        private void HandlePlayerDamage(PlayerDamageEvent e)
        {
            _canvasView.HPView.SetCountText(e.Value.newHP.ToString());
        }

        private void HandleEnemyKill(EnemyKilledEvent e)
        {
            _eventBus.RaiseEvent(new ScoreChangedEvent(e.Value.Config.KillReward));
        }
    }
}