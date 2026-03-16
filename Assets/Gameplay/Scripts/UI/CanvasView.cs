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

        [SerializeField]
        private GameObject _playingMenu;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(SetPause);
            _resumeButton.onClick.AddListener(SetResume);

            //_eventBus.Subscribe<ChangeGameStateEvent>(SetLoseWin);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(SetPause);
            _resumeButton.onClick.RemoveListener(SetResume);
        }

        public void Init()
        {
            gameObject.SetActive(true);

            _showEventsView.Init(_eventBus);

            _endGameView.Init(_eventBus);
        }

        public void ShowLose()
        {
            _endGameView.ShowWinLose(false);
            ShowPlayingMenu(false);
        }

        public void ShowWin()
        {
            _endGameView.ShowWinLose(true);
            ShowPlayingMenu(false);
        }

        public void ShowPlaying()
        {
            ShowPlayingMenu(true);
            SetPlaying(true);
            _endGameView.Hide();
        }

        private void ShowPlayingMenu(bool isPlay)
        {
            _playingMenu.SetActive(isPlay);
        }

        private void SetPlaying(bool isPlaying)
        {
            _pauseButton.gameObject.SetActive(isPlaying);

            _resumeButton.gameObject.SetActive(!isPlaying);

            //_eventBus.RaiseEvent(stateEvent);
            _showEventsView.gameObject.SetActive(!isPlaying);
        }

        private void SetPause()
        {
            SetPauseResume(true);
        }

        private void SetResume()
        {
            SetPauseResume(false);
        }

        private void SetPauseResume(bool isPause)
        {
            SetPlaying(!isPause);

            if (isPause)
            {
                _eventBus.RaiseEvent(new GamePausedEvent());
            }
            else
            {
                _eventBus.RaiseEvent(new GamePlayingEvent());
            }
        }
    }
}