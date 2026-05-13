using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class BootstrapInitializer : MonoBehaviour
    {
        public static BootstrapInitializer Instance => _instance;

        private static BootstrapInitializer _instance;

        public GameStateMachine StateMachine => _gameStateMachine;

        public GameResetService ResetService => _resetService;

        public IGameExit GameExit => _gameExit;

        [SerializeField]
        private PlayerMoveInputConfig _wasdMoveConfig;

        [SerializeField]
        private PlayerMoveInputConfig _aiMoveConfig;

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

            SceneManager.LoadSceneAsync(_gameSceneIndex);
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