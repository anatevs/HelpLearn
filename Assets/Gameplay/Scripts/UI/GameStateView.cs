using EventBusNamespace;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameStateView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private void OnEnable()
        {
            EventBus.Subscribe<GameInitEvent>(ShowState);
            EventBus.Subscribe<GamePlayingEvent>(ShowState);
            EventBus.Subscribe<GamePausedEvent>(ShowState);
            EventBus.Subscribe<GameWinEvent>(ShowState);
            EventBus.Subscribe<GameLoseEvent>(ShowState);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<GameInitEvent>(ShowState);
            EventBus.Unsubscribe<GamePlayingEvent>(ShowState);
            EventBus.Unsubscribe<GamePausedEvent>(ShowState);
            EventBus.Unsubscribe<GameWinEvent>(ShowState);
            EventBus.Unsubscribe<GameLoseEvent>(ShowState);
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