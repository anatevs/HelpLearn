using EventBusNamespace;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI
{
    public class GameStateView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private EventBus _eventBus;

        private bool _isSubscribed = false;

        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;

            Subscribe();
        }

        private void OnEnable()
        {
            if (_eventBus != null)
            {
                Subscribe();
            }
        }

        private void OnDisable()
        {
            if (_isSubscribed)
            {
                _eventBus.Unsubscribe<GameInitEvent>(ShowState);
                _eventBus.Unsubscribe<GamePlayingEvent>(ShowState);
                _eventBus.Unsubscribe<GamePausedEvent>(ShowState);
                _eventBus.Unsubscribe<GameWinEvent>(ShowState);
                _eventBus.Unsubscribe<GameLoseEvent>(ShowState);

                _isSubscribed = false;
            }
        }

        private void ShowState<T>(T e) where T : ChangeStateEvent
        {
            SetText(e.State.ToString());
        }

        private void SetText(string text)
        {
            _text.text = text;
        }

        private void Subscribe()
        {
            if (!_isSubscribed)
            {
                _eventBus.Subscribe<GameInitEvent>(ShowState);
                _eventBus.Subscribe<GamePlayingEvent>(ShowState);
                _eventBus.Subscribe<GamePausedEvent>(ShowState);
                _eventBus.Subscribe<GameWinEvent>(ShowState);
                _eventBus.Subscribe<GameLoseEvent>(ShowState);

                _isSubscribed = true;
            }
        }
    }
}