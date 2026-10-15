using EventBusNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        private EventBus _eventBus;

        private Button _button;

        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;

            _button = GetComponent<Button>();
        }

        public void Subscribe()
        {
            _button.onClick.AddListener(Restart);
        }

        public void Unsubscribe()
        {
            _button.onClick.RemoveListener(Restart);
        }

        private void Restart()
        {
            _eventBus.RaiseEvent(new RestartEvent());
        }
    }
}