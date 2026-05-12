using Gameplay;
using Input;
using UI;
using UnityEngine;

namespace GameManagement
{
    public sealed class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _player;

        [SerializeField]
        private Transform[] _patrolPoints;

        [SerializeField]
        private ItemsSpawner _itemsSpawner;

        [Header("Configs")]
        [SerializeField]
        private LogMessagesConfig _logMessagesConfig;

        [SerializeField]
        private PlayerMoveInputConfig _wasdMoveConfig;

        [SerializeField]
        private PlayerMoveInputConfig _aiMoveConfig;

        [Header("HUD views")]
        [SerializeField]
        private GameplayHud _gameplayHud;

        [SerializeField]
        private PauseResumeView _pauseResumeView;

        [Header("Menu views")]
        [SerializeField]
        private MainMenuView _mainMenuView;

        [SerializeField]
        private GameOverView _gameoverView;

        private IInputSwitchService _inputSwitchService;

        private InputSwitchBinder _inputSwitchBinder;

        private readonly int _initInputIndex = 0;

        private IHealth _playerHealth;

        private IItemsSceneService _itemsSceneService;

        private ICollectService _collectService;

        private CollectItemsBinder _collectBinder;

        private ILoggerService _loggerService;

        private GameplayHudController _hudController;

        private PauseResumePresenter _pauseResumePresenter;

        private GameResetService _resetService;

        private GameStateMachine _gameStateMachine;

        private IGameExit _gameExit;

        private MainMenuPresenter _mainMenuPresenter;

        private GameOverPresenter _gameOverPresenter;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _gameStateMachine = new GameStateMachine(new InitState());

            _gameExit = new GameExit();

            _resetService = new();

            InitInput();

            InitPlayer();

            InitItems();

            InitGameplayUI();

            InitMenuUI();

            _gameStateMachine.ChangeState(new MainMenuState(_mainMenuPresenter));
        }

        private void InitInput()
        {
            IInputService[] inputServices = new IInputService[2];
            inputServices[0] = new WASDInputService(_wasdMoveConfig);
            inputServices[1] = new AIInputService(_aiMoveConfig, _patrolPoints);

            _inputSwitchService = new InputSwitchService(inputServices, _initInputIndex);

            _resetService.AddResetable(_inputSwitchService);
            _gameExit.AddDisposable(_inputSwitchService);
        }

        private void SetTestInput(Vector3 testDirection)
        {
            var testInput = new TestInputService(_wasdMoveConfig, testDirection);
            _inputSwitchService.SwitchInput(testInput);
        }

        private void InitPlayer()
        {
            var movement = new MovementRB(_player.gameObject);
            var rotation = new RotationLerp(_player.transform);

            _playerHealth = new SimpleHP(_player.Config.StartHP);

            _playerHealth.OnKilled += HandlePlayerKill;

            _player.Init(_inputSwitchService.CurrentInput, movement, rotation, _playerHealth);

            _resetService.AddResetable(_player);
        }

        private void InitItems()
        {
            _itemsSpawner.Init();

            _itemsSceneService = new ItemsSceneService(_itemsSpawner);

            _collectService = new CollectService(_player.Config.InitShowedItems);

            _collectBinder = new CollectItemsBinder(_itemsSceneService, _collectService);

            _resetService.AddResetable(_itemsSpawner);
            _resetService.AddResetable(_itemsSceneService);
            _resetService.AddResetable(_collectService);

            _gameExit.AddDisposable(_itemsSceneService);
            _gameExit.AddDisposable(_collectBinder);
        }

        private void InitGameplayUI()
        {
            _loggerService = new LoggerService(_logMessagesConfig, _playerHealth, _collectService);

            _gameExit.AddDisposable(_loggerService);

            _hudController = new GameplayHudController(_gameplayHud, _collectService,
                _playerHealth, _inputSwitchService);

            _gameExit.AddDisposable(_hudController);

            _inputSwitchBinder = new InputSwitchBinder(_inputSwitchService, _player, _loggerService);

            _gameExit.AddDisposable(_inputSwitchBinder);

            _pauseResumePresenter = new PauseResumePresenter(_pauseResumeView, _gameStateMachine);

            _gameExit.AddDisposable(_pauseResumePresenter);
        }

        private void InitMenuUI()
        {
            _mainMenuPresenter = new MainMenuPresenter(_mainMenuView, _resetService, _gameStateMachine, _gameExit);

            _gameExit.AddDisposable(_mainMenuPresenter);

            _gameOverPresenter = new GameOverPresenter(_gameoverView, _mainMenuPresenter, _resetService, _gameStateMachine);

            _gameExit.AddDisposable(_gameOverPresenter);
        }

        private void HandlePlayerKill()
        {
            _gameStateMachine.ChangeState(new GameOverState(false, _gameOverPresenter));
        }

        private void OnDisable()
        {
            _playerHealth.OnKilled -= HandlePlayerKill;
        }

        private void OnApplicationQuit()
        {
            _gameExit.QuitGame();
        }
    }
}