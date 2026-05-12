using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class PauseResumeView : MonoBehaviour,
        IPauseResumeView
    {
        public event Action OnPaused;
        public event Action OnResumed;

        [SerializeField]
        private Button _pauseButton;

        [SerializeField]
        private Button _resumeButton;

        [SerializeField]
        private Image _pausedImage;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(ShowPaused);
            _resumeButton.onClick.AddListener(ShowResumed);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(ShowPaused);
            _resumeButton.onClick.RemoveListener(ShowResumed);
        }

        public void ShowResumed()
        {
            SwitchView(false);
            OnResumed?.Invoke();
        }

        public void ShowPaused()
        {
            SwitchView(true);
            OnPaused?.Invoke();
        }

        private void SwitchView(bool isPaused)
        {
            _pauseButton.gameObject.SetActive(!isPaused);

            _resumeButton.gameObject.SetActive(isPaused);
            _pausedImage.enabled = isPaused;
        }
    }
}