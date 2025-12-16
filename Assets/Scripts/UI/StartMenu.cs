using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameCore
{
    public sealed class StartMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;

        private int _gameSceneIndex = 1;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(_gameSceneIndex);
        }
    }
}