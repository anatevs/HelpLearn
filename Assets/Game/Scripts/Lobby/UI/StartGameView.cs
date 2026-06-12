using System;
using UnityEngine;
using UnityEngine.UI;

namespace Network.UI
{
    public class StartGameView : MonoBehaviour
    {
        public event Action OnStartClicked;

        [SerializeField]
        private Button _startGameButton;

        private void OnEnable()
        {
            EnableButton(false);

            _startGameButton.onClick.AddListener(HandleStartClick);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(HandleStartClick);
        }

        public void EnableButton(bool enable)
        {
            _startGameButton.interactable = enable;
        }

        private void HandleStartClick()
        {
            OnStartClicked?.Invoke();
        }
    }
}