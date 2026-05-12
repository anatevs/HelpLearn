using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverView : MonoBehaviour,
        IGameOverView
    {
        public event Action OnToMainMenu;

        public IRestartGameView RestartView => _restartGameView;

        [SerializeField]
        private TMP_Text _winText;

        [SerializeField]
        private TMP_Text _loseText;

        [SerializeField]
        private Button _mainMenuButton;

        [SerializeField]
        private RestartGameView _restartGameView;

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(HandleMenuClick);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(HandleMenuClick);
        }

        public void Show(bool isWin)
        {
            gameObject.SetActive(true);

            _winText.gameObject.SetActive(isWin);
            _loseText.gameObject.SetActive(!isWin);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void HandleMenuClick()
        {
            OnToMainMenu?.Invoke();
        }
    }
}