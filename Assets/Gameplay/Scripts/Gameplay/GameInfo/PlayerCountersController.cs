using EventBusNamespace;
using GameManagement;
using UI;

namespace Gameplay
{
    public sealed class PlayerCountersController : DDOLClass<PlayerCountersController>
    {
        private Player _player;

        private int _startScore = 0;
        private int _startPicked = 0;

        private ScoreManager _scoreManager;

        private CounterManager<ItemPickedEvent> _pickedManager;

        private void OnEnable()
        {
            EventBus.Subscribe<EnemyKilledEvent>(HandleEnemyKill);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<EnemyKilledEvent>(HandleEnemyKill);
        }

        public void Init(Player player)
        {
            _player = player;

            _scoreManager = new(_startScore, CanvasView.Instance.ScoreView, player.Config.WinScore);

            _pickedManager = new(_startPicked, CanvasView.Instance.PickedItemsView);

            Reset();
        }

        public void Reset()
        {
            _scoreManager.Reset(_startScore);
            _pickedManager.Reset(_startPicked);

            CanvasView.Instance.HPView.SetCountText(_player.Config.HP.ToString());
        }

        private void HandleEnemyKill(EnemyKilledEvent e)
        {
            EventBus.RaiseEvent(new ScoreChangedEvent(e.Value.Config.KillReward));
        }
    }
}