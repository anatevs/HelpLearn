using EventBusNamespace;
using GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class CanvasView : DDOLClass<CanvasView>
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

        [SerializeField]
        private Button _pauseButton;

        [SerializeField]
        private Button _resumeButton;

        [SerializeField]
        private ShowEventsView _showEventsView;

        [SerializeField]
        private EndGameView _endGameView;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(SetPause);
            _resumeButton.onClick.AddListener(SetResume);

            EventBus.Subscribe<ChangeGameStateEvent>(SetLoseWin);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(SetPause);
            _resumeButton.onClick.RemoveListener(SetResume);

            EventBus.Subscribe<ChangeGameStateEvent>(SetLoseWin);
        }

        public void Init()
        {
            gameObject.SetActive(true);
        }

        private void SetPause()
        {
            SetPause(true);
        }

        private void SetResume()
        {
            SetPause(false);
        }

        private void SetPause(bool isPause)
        {
            _pauseButton.gameObject.SetActive(!isPause);

            _resumeButton.gameObject.SetActive(isPause);

            _showEventsView.gameObject.SetActive(isPause);

            var stateEvent = isPause?
                new ChangeGameStateEvent(GameState.Paused) :
                new ChangeGameStateEvent(GameState.Playing);

            EventBus.RaiseEvent(stateEvent);
        }

        private void SetLoseWin(ChangeGameStateEvent stateEvent)
        {
            if (stateEvent.Value == GameState.Lose)
            {
                _endGameView.ShowWinLose(false);
            }
            else if (stateEvent.Value == GameState.Win)
            {
                _endGameView.ShowWinLose(true);
            }
            else
            {
                _endGameView.Hide();
            }
        }
    }
}