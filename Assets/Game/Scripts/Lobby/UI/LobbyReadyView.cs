using System;
using UnityEngine;
using UnityEngine.UI;

namespace Network.UI
{
    public class LobbyReadyView : MonoBehaviour
    {
        public event Action<bool> OnReadyChanged;

        [SerializeField]
        private Button _readyButton;

        [SerializeField]
        private Button _unredyButton;

        private void OnEnable()
        {
            _readyButton.onClick.AddListener(HandleReady);
            _unredyButton.onClick.AddListener(HandleCancel);
        }

        private void OnDisable()
        {
            _readyButton.onClick.RemoveListener(HandleReady);
            _unredyButton.onClick.RemoveListener(HandleCancel);
        }

        public void SwitchViewButton(bool isReady)
        {
            _readyButton.gameObject.SetActive(!isReady);
            _unredyButton.gameObject.SetActive(isReady);
        }

        private void HandleReady()
        {
            ChangeReady(true);
        }

        private void HandleCancel()
        {
            ChangeReady(false);
        }

        private void ChangeReady(bool isReady)
        {
            OnReadyChanged?.Invoke(isReady);
            SwitchViewButton(isReady);
        }
    }
}