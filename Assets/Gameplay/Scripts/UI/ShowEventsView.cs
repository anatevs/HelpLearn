using EventBusNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class ShowEventsView : MonoBehaviour
    {
        [SerializeField]
        private Button _showAllButton;

        [SerializeField]
        private TMP_InputField _amountInputField;

        private ShowEventsConsole _showEventsConsole;

        private void OnEnable()
        {
            _showAllButton.onClick.AddListener(ShowAllEvents);

            _amountInputField.onEndEdit.AddListener(ShowNEvents);
        }

        private void OnDisable()
        {
            _showAllButton.onClick.RemoveListener(ShowAllEvents);

            _amountInputField.onEndEdit.RemoveListener(ShowNEvents);
        }

        public void Init(EventBus eventBus)
        {
            _showEventsConsole = new ShowEventsConsole(eventBus);
        }

        private void ShowAllEvents()
        {
            _showEventsConsole.ShowAll();
        }

        private void ShowNEvents(string value)
        {
            if (int.TryParse(value, out var n))
            {
                _showEventsConsole.ShowLastN(n);
            }
        }
    }
}