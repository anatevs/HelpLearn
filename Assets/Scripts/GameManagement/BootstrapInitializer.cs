using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    [DefaultExecutionOrder(-50)]
    public class BootstrapInitializer : MonoBehaviour
    {
        public static BootstrapInitializer Instance => _instance;

        private static BootstrapInitializer _instance;

        public GameStateMachine StateMachine => _gameStateMachine;

        public GameResetService ResetService => _resetService;

        public IGameExit GameExit => _gameExit;

        private int _gameSceneIndex = 1;

        private GameResetService _resetService;

        private GameStateMachine _gameStateMachine;

        private IGameExit _gameExit;

        private void Awake()
        {
            if (BootstrapInitializer.Instance != null)
            {
                Destroy(gameObject);

                return;
            }

            _instance = this;

            DontDestroyOnLoad(gameObject);

            Init();

            if (SceneManager.GetActiveScene().buildIndex != _gameSceneIndex)
            {
                SceneManager.LoadSceneAsync(_gameSceneIndex);
            }
        }

        private void Init()
        {
            _gameStateMachine = new GameStateMachine(new InitState());

            _gameExit = new GameExit();

            _resetService = new();
        }

        private void OnApplicationQuit()
        {
            _gameExit.QuitGame();
        }
    }
}