using Events;
using Gameplay;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EventsInfoView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private PrintEventsConfig _config;

        private bool _isFilterShowing = false;

        private void Awake()
        {
            _config.Init();
        }

        private void OnEnable()
        {
            GameSingleton.Instance.EventManager.OnGameEvent += ShowText;
        }

        private void OnDisable()
        {
            GameSingleton.Instance.EventManager.OnGameEvent -= ShowText;
        }


        private void ShowText(GameEvent gameEvent)
        {
            if (!_isFilterShowing)
            {
                ShowText(gameEvent.FormatInfoString(_config.GetColorHex(gameEvent.Type)));
            }
        }

        private void ShowText(string text)
        {
            _text.text = text;
        }
    }
}