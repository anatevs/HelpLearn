using EventBusNamespace;
using GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class CanvasView : MonoBehaviour
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

        [SerializeField]
        private EventBus _eventBus;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(SetPause);
            _resumeButton.onClick.AddListener(SetResume);

            _eventBus.Subscribe<ChangeGameStateEvent>(SetLoseWin);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(SetPause);
            _resumeButton.onClick.RemoveListener(SetResume);

            _eventBus.Subscribe<ChangeGameStateEvent>(SetLoseWin);
        }

        public void Init()
        {
            gameObject.SetActive(true);

            _showEventsView.Init(_eventBus);

            _endGameView.Init(_eventBus);
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

            _eventBus.RaiseEvent(stateEvent);
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