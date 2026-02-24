using GameManagement;
using UnityEngine;

namespace UI
{
    public class CanvasView : DDOLClass<CanvasView>
    {
        public CounterView ScoreView => _scoreView;
        public CounterView PickedItemsView => _pickedItemsView;
        public CounterView HPView => _hpView;

        [SerializeField]
        private CounterView _scoreView;

        [SerializeField]
        private CounterView _pickedItemsView;

        [SerializeField]
        private CounterView _hpView;
    }
}