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

        private bool _isSubscribed = false;

        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;

            if (!_isSubscribed)
            {
                _button = GetComponent<Button>();
                _button.onClick.AddListener(Restart);

                _isSubscribed = true;
            }
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            if (_eventBus != null && !_isSubscribed)
            {
                _button.onClick.AddListener(Restart);

                _isSubscribed = true;
            }
        }

        private void OnDisable()
        {
            if (_isSubscribed)
            {
                _button.onClick.RemoveListener(Restart);

                _isSubscribed = false;
            }
        }

        private void Restart()
        {
            _eventBus.RaiseEvent(new RestartEvent());
        }
    }
}