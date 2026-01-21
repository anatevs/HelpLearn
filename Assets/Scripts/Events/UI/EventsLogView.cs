using Events;
using Gameplay;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EventsLogView : MonoBehaviour
    {
        public bool IsLogsShow { set => _isLogsShow = value; }

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private PrintEventsConfig _config;

        private bool _isLogsShow = true;

        private void Awake()
        {
            _config.Init();
        }

        private void OnEnable()
        {
            GameSingleton.Instance.EventManager.OnGameEvent += ShowNewEvent;
        }

        private void OnDisable()
        {
            GameSingleton.Instance.EventManager.OnGameEvent -= ShowNewEvent;
        }

        public void ShowEvents(IEnumerable<GameEvent> events, string prefix = "")
        {
            var wholeString = prefix;
            var count = 0;

            foreach (GameEvent gameEvent in events)
            {
                count++;
                wholeString += $"{GetEventString(gameEvent, $"{count}. ")}\n";
            }

            if (count == 0)
            {
                ShowText($"No events found");
                return;
            }

            ShowText(wholeString);
        }

        public void ShowLastEvent()
        {
            if (GameSingleton.Instance.EventManager.Events.Count > 0)
            {
                ShowText(GetEventString(GameSingleton.Instance.EventManager.Events[^1]));
                return;
            }

            ShowText("");
        }

        public void ShowText(string text)
        {
            _text.text = text;
        }

        private void ShowNewEvent(GameEvent gameEvent)
        {
            if (_isLogsShow)
            {
                ShowText(GetEventString(gameEvent));
            }
        }

        private string GetEventString(GameEvent gameEvent, string prefix = "")
        {
            return gameEvent.FormatInfoString(_config.GetColorHex(gameEvent.Type), prefix);
        }
    }
}