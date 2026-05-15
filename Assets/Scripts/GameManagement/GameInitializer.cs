using Gameplay;
using Input;
using UI;
using UnityEngine;
using System.Collections.Generic;

namespace GameManagement
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _player;

        [SerializeField]
        private ItemsSpawner _itemsSpawner;

        [SerializeField]
        private GameplayController _gameplayController;

        [Header("Configs")]
        [SerializeField]
        private LogMessagesConfig _logMessagesConfig;

        [SerializeField]
        private PlayerMoveInputConfig _wasdMoveConfig;

        [SerializeField]
        private PlayerMoveInputConfig _aiMoveConfig;

        [SerializeField]
        private AIPatrolPointsPrefab _pointsPrefab;

        [SerializeField]
        private GameModifierConfig[] _modifierConfigs;

        [Header("HUD views")]
        [SerializeField]
        private GameplayHud _gameplayHud;

        [SerializeField]
        private PauseResumeView _pauseResumeView;

        [SerializeField]
        private ModifiersInfoView _modifiersInfoView;

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

        private ModifiersCreator _modifiersCreator;

        private ILoggerService _loggerService;

        private GameplayHudController _hudController;

        private PauseResumePresenter _pauseResumePresenter;

        private GameResetService _resetService;

        private GameStateMachine _gameStateMachine;

        private IGameExit _gameExit;

        private MainMenuPresenter _mainMenuPresenter;

        private GameOverPresenter _gameOverPresenter;

        private ModifiersPresenter _modifiersPresenter;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _gameStateMachine = BootstrapInitializer.Instance.StateMachine;

            _gameExit = BootstrapInitializer.Instance.GameExit;

            _resetService = BootstrapInitializer.Instance.ResetService;

            InitInput();

            InitPlayer();

            InitItems();

            InitModifiers();

            InitGameplayUI();

            InitMenuUI();

            _gameStateMachine.ChangeState(new MainMenuState(_mainMenuPresenter));
        }

        private void InitInput()
        {
            IInputService[] inputServices = new IInputService[2];
            inputServices[0] = new WASDInputService(_wasdMoveConfig);
            inputServices[1] = new AIInputService(_aiMoveConfig, _pointsPrefab);

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

        private void InitModifiers()
        {
            List<IModifierFactory> factories = new();

            factories.Add(new SpeedModifierFactory(_inputSwitchService));


            _modifiersCreator = new();

            _modifiersCreator.AddFactory(factories);


            var modifiers = _modifiersCreator.CreateModifiers(_modifierConfigs);

            _gameplayController.Init(modifiers);
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

            _modifiersPresenter = new ModifiersPresenter(_modifiersInfoView, _gameplayController);

            _gameExit.AddDisposable(_modifiersPresenter);
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
    }
}