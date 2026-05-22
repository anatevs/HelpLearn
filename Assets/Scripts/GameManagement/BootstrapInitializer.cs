using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class BootstrapInitializer : MonoBehaviour
    {
        private static bool _isInitialized = false;

        private int _gameSceneIndex = 1;

        private GameResetService _resetService;

        private GameStateMachine _gameStateMachine;

        private GameStatesService _gameStatesService;

        private IGameExit _gameExit;

        private void Awake()
        {
            if (_isInitialized)
            {
                Destroy(gameObject);

                return;
            }

            _isInitialized = true;

            DontDestroyOnLoad(gameObject);

            Init();

            if (SceneManager.GetActiveScene().buildIndex != _gameSceneIndex)
            {
                SceneManager.sceneLoaded += HandleGameSceneLoad;
                SceneManager.LoadScene(_gameSceneIndex);
            }
            else
            {
                InitGameScene();
            }
        }

        private void Init()
        {
            _gameStateMachine = new GameStateMachine(new InitState());

            _gameStatesService = new GameStatesService(_gameStateMachine);

            _gameExit = new GameExit();

            _resetService = new();
        }

        private void HandleGameSceneLoad(Scene scene, LoadSceneMode mode)
        {
            if (SceneManager.GetActiveScene().buildIndex == _gameSceneIndex)
            {
                InitGameScene();

                SceneManager.sceneLoaded -= HandleGameSceneLoad;
            }
        }

        private void InitGameScene()
        {
            var gameInitializer = FindFirstObjectByType<GameInitializer>();

            gameInitializer.Init(_gameStateMachine, _gameStatesService, _resetService, _gameExit);
        }

        private void OnApplicationQuit()
        {
            _gameExit.QuitGame();
        }
    }
}