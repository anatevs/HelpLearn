using UnityEngine;
using EventBusNamespace;

namespace UI
{
    public sealed class EndGameView : MonoBehaviour
    {
        [SerializeField]
        private RestartButton _restartButton;

        [SerializeField]
        private GameObject _loseView;

        [SerializeField]
        private GameObject _winView;

        private EventBus _eventBus;

        public void Init(EventBus eventBus)
        {
            _eventBus = eventBus;

            _restartButton.Construct(_eventBus);
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
    }
}