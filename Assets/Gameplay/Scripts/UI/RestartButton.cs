using EventBusNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField]
        private EventBus _eventBus;

        private Button _restartButton;

        private void Awake()
        {
            _restartButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(Restart);
        }

        private void Restart()
        {
            _eventBus.RaiseEvent(new RestartEvent());
        }
    }
}