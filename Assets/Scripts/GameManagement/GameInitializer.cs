using Gameplay;
using Input;
using System;
using System.Collections.Generic;
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

        [SerializeField]
        private LogMessagesConfig _logMessagesConfig;

        [SerializeField]
        private PlayerMoveInputConfig _wasdMoveConfig;

        [SerializeField]
        private PlayerMoveInputConfig _aiMoveConfig;

        [SerializeField]
        private GameplayHud _gameplayHud;

        private IInputSwitchService _inputSwitchService;

        private InputSwitchBinder _inputSwitchBinder;

        private readonly int _initInputIndex = 0;

        private IHealth _playerHealth;

        private IItemsSceneService _itemsService;

        private ICollectService _collectService;

        private CollectItemsBinder _collectBinder;

        private ILoggerService _loggerService;

        private GameplayHudController _hudController;

        private readonly List<IResetable> _resetables = new();

        private readonly List<IDisposable> _disposables = new();

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            InitInput();

            InitPlayer();

            InitItems();

            _loggerService = new LoggerService(_logMessagesConfig, _playerHealth, _collectService);

            _disposables.Add(_loggerService);

            _hudController = new GameplayHudController(_gameplayHud, _collectService,
                _playerHealth, _inputSwitchService);

            _disposables.Add(_hudController);

            _inputSwitchBinder = new InputSwitchBinder(_inputSwitchService, _player, _loggerService);

            _disposables.Add(_inputSwitchBinder);

            _itemsSpawner.ResetLevel();
        }

        private void InitInput()
        {
            IInputService[] inputServices = new IInputService[2];
            inputServices[0] = new WASDInputService(_wasdMoveConfig);
            inputServices[1] = new AIInputService(_aiMoveConfig, _patrolPoints);

            _inputSwitchService = new InputSwitchService(inputServices, _initInputIndex);

            _resetables.Add(_inputSwitchService);
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

            _player.Init(_inputSwitchService.CurrentInput, movement, rotation, _playerHealth);
        }

        private void InitItems()
        {
            _itemsSpawner.Init();

            _itemsService = new ItemsSceneService(_itemsSpawner);

            _collectService = new CollectService(_player.Config.InitShowedItems);

            _collectBinder = new CollectItemsBinder(_itemsService, _collectService);

            _resetables.Add(_itemsSpawner);
            _resetables.Add(_collectService);

            _disposables.Add(_itemsService);
            _disposables.Add(_collectBinder);
        }

        private void ResetLevel()
        {
            foreach (var resetable in _resetables)
            {
                resetable.ResetLevel();
            }
        }

        private void OnDisable()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}