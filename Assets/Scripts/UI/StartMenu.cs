using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameCore
{
    public sealed class StartMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _quitButton;

        [SerializeField]
        private TMP_Text _recordText;

        private int _gameSceneIndex = 1;

        public void ShowRecord(int hits, int shots)
        {
            _recordText.gameObject.SetActive(true);

            _recordText.text = $"Previous hits record:\n{hits} hits from {shots} shoots";
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
            _quitButton.onClick.AddListener(QuitGame);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
            _quitButton.onClick.RemoveListener(QuitGame);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(_gameSceneIndex);
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }
    }
}