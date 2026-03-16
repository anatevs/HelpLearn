using EventBusNamespace;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameStateView : MonoBehaviour
    {
        [SerializeField]
        private EventBus _eventBus;

        [SerializeField]
        private TMP_Text _text;

        private void OnEnable()
        {
            _eventBus.Subscribe<GameInitEvent>(ShowState);
            _eventBus.Subscribe<GamePlayingEvent>(ShowState);
            _eventBus.Subscribe<GamePausedEvent>(ShowState);
            _eventBus.Subscribe<GameWinEvent>(ShowState);
            _eventBus.Subscribe<GameLoseEvent>(ShowState);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<GameInitEvent>(ShowState);
            _eventBus.Unsubscribe<GamePlayingEvent>(ShowState);
            _eventBus.Unsubscribe<GamePausedEvent>(ShowState);
            _eventBus.Unsubscribe<GameWinEvent>(ShowState);
            _eventBus.Unsubscribe<GameLoseEvent>(ShowState);
        }

        private void ShowState<T>(T e) where T : ChangeStateEvent
        {
            SetText(e.State.ToString());
        }

        private void SetText(string text)
        {
            _text.text = text;
        }
    }
}