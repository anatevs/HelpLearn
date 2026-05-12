using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuView : MonoBehaviour,
        IMainMenuView
    {
        public IRestartGameView RestartView => _restartGameView;

        public event Action OnExitClicked;

        [SerializeField]
        private RestartGameView _restartGameView;

        [SerializeField]
        private Button _exitButton;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(HandleExitClick);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(HandleExitClick);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void HandleExitClick()
        {
            OnExitClicked?.Invoke();
        }
    }
}