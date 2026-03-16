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

        [SerializeField]
        private Button _spawnedEnemiesButton;

        private ShowEventsConsole _showEventsConsole;

        private void OnEnable()
        {
            _showAllButton.onClick.AddListener(ShowAllEvents);

            _amountInputField.onEndEdit.AddListener(ShowNEvents);

            _spawnedEnemiesButton.onClick.AddListener(ShowSpawnedEnemies);
        }

        private void OnDisable()
        {
            _showAllButton.onClick.RemoveListener(ShowAllEvents);

            _amountInputField.onEndEdit.RemoveListener(ShowNEvents);

            _spawnedEnemiesButton.onClick.RemoveListener(ShowSpawnedEnemies);
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

        private void ShowSpawnedEnemies()
        {
            _showEventsConsole.ShowSpawnedEnemiesAmount();
        }
    }
}