using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RestartGameView : MonoBehaviour,
        IRestartGameView
    {
        public event Action OnRestartClicked;

        [SerializeField]
        private Button _menuButton;

        private void OnEnable()
        {
            _menuButton.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            OnRestartClicked?.Invoke();
        }
    }
}