using Events;
using Gameplay;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using EventType = Events.EventType;

namespace UI
{
    public class FilterOprtionsView : MonoBehaviour
    {
        [SerializeField]
        private EventsLogView _logView;

        [SerializeField]
        private Button _byTypeButton;

        [SerializeField]
        private Button _pickedAmountButton;

        [SerializeField]
        private Button _mostFrequentlyButton;

        [SerializeField]
        private Button _lastEventsButton;

        [SerializeField]
        private GameObject _enterMenu;

        [SerializeField]
        private SelectEventTypeView _selectTypeMenu;

        [SerializeField]
        private LastAmountMenu _lastEventsMenu;


        private void OnEnable()
        {
            _byTypeButton.onClick.AddListener(ShowSelectType);

            _pickedAmountButton.onClick.AddListener(LogPickedEvents);

            _mostFrequentlyButton.onClick.AddListener(LogMostFrequently);

            _lastEventsButton.onClick.AddListener(ShowLastAmount);
        }

        private void OnDisable()
        {
            _byTypeButton.onClick.RemoveListener(ShowSelectType);

            _pickedAmountButton.onClick.RemoveListener(LogPickedEvents);

            _mostFrequentlyButton.onClick.RemoveListener(LogMostFrequently);

            _lastEventsButton.onClick.RemoveListener(ShowLastAmount);
        }

        public void Show(bool isShow)
        {
            gameObject.SetActive(isShow);
            ShowEnterMenu(isShow);
        }

        private void ShowEnterMenu(bool isShow)
        {
            _enterMenu.SetActive(isShow);
        }

        private void ShowSelectType()
        {
            _selectTypeMenu.gameObject.SetActive(true);
            _selectTypeMenu.OnEndClicked += LogTypeEvents;
            ShowEnterMenu(false);
        }

        private void ShowLastAmount()
        {
            _lastEventsMenu.gameObject.SetActive(true);
            _lastEventsMenu.OnAmountSet += LogLastEvents;
            ShowEnterMenu(false);
        }

        private void LogTypeEvents(EventType type)
        {
            _logView.ShowEvents(EventsSelector.GetByType(
                GameSingleton.Instance.EventManager.Events, type));

            _selectTypeMenu.OnEndClicked -= LogTypeEvents;

            gameObject.SetActive(false);
        }

        private void LogPickedEvents()
        {
            var amount = EventsSelector.GetPickedItemsCount(
                GameSingleton.Instance.EventManager.Events);

            _logView.ShowText($"Number of item picked events: {amount}");

            gameObject.SetActive(false);
        }

        private void LogMostFrequently()
        {
            var events = EventsSelector.GetMostFrequently(
                GameSingleton.Instance.EventManager.Events, out var type);

            var title = "";

            if (events.Count() > 0)
            {
                title = $"The most frequently events are {type}\n";
            }

            _logView.ShowEvents(events, title);

            gameObject.SetActive(false);
        }

        private void LogLastEvents(int amount)
        {
            _logView.ShowEvents(EventsSelector.GetLastEvents(
                GameSingleton.Instance.EventManager.Events, amount));

            _lastEventsMenu.OnAmountSet -= LogLastEvents;

            gameObject.SetActive(false);
        }
    }
}