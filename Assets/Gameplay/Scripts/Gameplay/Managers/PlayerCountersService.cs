using EventBusNamespace;
using GameManagement;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class PlayerCountersService : DDOLClass<PlayerCountersService>
    {
        [SerializeField]
        private GameConfig _gameConfig;

        private int _startScore = 0;
        private int _startPicked = 0;

        private CounterManager<EnemyKilledEvent> _scoreManager;

        private CounterManager<ItemPickedEvent> _pickedManager;

        public void Init()
        {
            _scoreManager = new(_startScore, CanvasView.Instance.ScoreView);

            _pickedManager = new(_startPicked, CanvasView.Instance.PickedItemsView);
        }
    }
}