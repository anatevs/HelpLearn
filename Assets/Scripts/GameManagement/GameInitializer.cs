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
        private GameModifiersConfig _gameplayModifiers;

        [Header("HUD views")]
        [SerializeField]
        private GameplayHud _gameplayHud;

        [SerializeField]
        private PauseResumeView _pauseResumeView;

        [SerializeField]
        private ModifiersInfoView _modifiersInfoView;

        [SerializeField]
        private SetModifiersView _setModifiersView;

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

        private IItemFactory _itemFactory;

        private ICollectService _collectService;

        private CollectItemsBinder _collectBinder;

        private ModifiersCreator _modifiersCreator;

        private ModifiersSpawner _modifiersSpawner;

        private ILoggerService _loggerService;

        private GameplayHudController _hudController;

        private PauseResumePresenter _pauseResumePresenter;

        private SceneItemsPresenter _sceneItemsPresenter;

        private GameResetService _resetService;

        private GameStateMachine _gameStateMachine;

        private GameStatesService _gameStatesService;

        private IGameExit _gameExit;

        private MainMenuPresenter _mainMenuPresenter;

        private GameOverPresenter _gameOverPresenter;

        private ModifiersInfoPresenter _modifiersPresenter;

        private SetModifiersPresenter _setModifiersPresenter;

        private void OnDisable()
        {
            _playerHealth.OnKilled -= HandlePlayerKill;
        }

        public void Init(GameStateMachine gameStateMachine,
            GameStatesService gameStatesService,
            GameResetService resetService,
            IGameExit gameExit)
        {
            _gameStateMachine = gameStateMachine;
            _gameStatesService = gameStatesService;
            _resetService = resetService;
            _gameExit = gameExit;

            InitInput();

            InitPlayer();

            InitItems();

            InitModifiers();

            InitGameplayUI();

            InitMenuUI();

            _gameStatesService.SetMainMenu(_mainMenuPresenter);
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

            _playerHealth = new SimpleHP(_player.Config.StartHP, _player.Config.MaxHP);

            _playerHealth.OnKilled += HandlePlayerKill;

            _player.Init(_inputSwitchService.CurrentInput, movement, rotation, _playerHealth);

            _resetService.AddResetable(_player);
        }

        private void InitItems()
        {
            _itemFactory = new ItemFactory();

            _itemsSpawner.Init(_itemFactory);

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
            _modifiersCreator = new();

            List<IModifierFactory> factories = new();

            factories.Add(new SpeedModifierFactory(_inputSwitchService));
            factories.Add(new RegenHPModifierFactory(_playerHealth));
            factories.Add(new FastSpawnModifierFactory(_itemsSpawner));

            _modifiersCreator.AddFactory(factories);


            _modifiersSpawner = new ModifiersSpawner(_modifiersCreator);

            _gameplayController.Init(_modifiersSpawner);

            _resetService.AddResetable(_gameplayController);
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

            _pauseResumePresenter = new PauseResumePresenter(_pauseResumeView, _gameStatesService);

            _gameExit.AddDisposable(_pauseResumePresenter);

            _modifiersPresenter = new ModifiersInfoPresenter(_modifiersInfoView, _gameplayController, _loggerService);

            _gameExit.AddDisposable(_modifiersPresenter);

            _sceneItemsPresenter = new SceneItemsPresenter(_loggerService, _itemsSpawner);

            _gameExit.AddDisposable(_sceneItemsPresenter);

            _setModifiersPresenter = new SetModifiersPresenter(_setModifiersView, _gameplayController, _gameplayModifiers);

            _gameExit.AddDisposable(_setModifiersPresenter);
        }

        private void InitMenuUI()
        {
            var mainMenuRestartPresenter = new RestartGamePresenter(_mainMenuView.RestartView, _resetService, _gameStatesService);
            _gameExit.AddDisposable(mainMenuRestartPresenter);

            _mainMenuPresenter = new MainMenuPresenter(_mainMenuView, _gameExit);

            _gameExit.AddDisposable(_mainMenuPresenter);

            var gameOverRestartPresenter = new RestartGamePresenter(_gameoverView.RestartView, _resetService, _gameStatesService);
            _gameExit.AddDisposable(gameOverRestartPresenter);

            _gameOverPresenter = new GameOverPresenter(_gameoverView, _mainMenuPresenter, _gameStatesService);

            _gameExit.AddDisposable(_gameOverPresenter);
        }

        private void HandlePlayerKill()
        {
            _gameStatesService.SetLost(_gameOverPresenter);
        }
    }
}