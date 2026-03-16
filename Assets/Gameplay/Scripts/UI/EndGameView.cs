using UnityEngine;
using UnityEngine.UI;
using EventBusNamespace;

namespace UI
{
    public sealed class EndGameView : MonoBehaviour
    {
        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private GameObject _loseView;

        [SerializeField]
        private GameObject _winView;

        private EventBus _eventBus;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(Restart);
        }

        public void Init(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void ShowWinLose(bool isWin)
        {
            _loseView.SetActive(!isWin);
            _winView.SetActive(isWin);

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Restart()
        {
            _eventBus.RaiseEvent(new RestartEvent());
        }
    }
}