using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class RestartGameController : MonoBehaviour
    {
        public event Action OnRestartClicked;

        [SerializeField]
        private Button[] _restartButtons;

        private void OnEnable()
        {
            foreach (var button in _restartButtons)
            {
                button.onClick.AddListener(HandleClick);
            }
        }

        private void OnDisable()
        {
            foreach (var button in _restartButtons)
            {
                button.onClick.RemoveListener(HandleClick);
            }
        }

        private void HandleClick()
        {
            OnRestartClicked?.Invoke();
        }
    }
}