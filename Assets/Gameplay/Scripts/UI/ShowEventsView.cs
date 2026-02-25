using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShowEventsView : MonoBehaviour
    {
        [SerializeField]
        private Button _showAllButton;

        [SerializeField]
        private TMP_InputField _amountInputField;

        private readonly ShowEventsConsole _showEventsConsole = new();

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