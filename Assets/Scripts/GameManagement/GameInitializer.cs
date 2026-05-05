using Assets.Input;
using Gameplay;
using Scripts.Input;
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
        private SwitchInputController _inputSwitchController;

        [SerializeField]
        private PlayerMoveInputConfig _wasdMoveConfig;

        [SerializeField]
        private PlayerMoveInputConfig _aiMoveConfig;

        private IInputService _input;

        private IInputService[] _inputServices;

        private readonly int _initInputIndex = 0;
        private int _currentInputIndex = 0;

        private IHealth _playerHealth;

        private IItemsSceneService _itemsService;

        private ICollectService _collectService;

        private CollectController _collectController;

        private ILoggerService _loggerService;

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

            _inputSwitchController.OnInputSwitched += SwitchInput;

            _itemsSpawner.ResetLevel();

            //SetTestInput(Vector3.right);
        }

        private void InitInput()
        {
            _inputServices = new IInputService[2];
            _inputServices[0] = new InputHandler(_wasdMoveConfig);
            _inputServices[1] = new AIInputService(_aiMoveConfig, _patrolPoints);
            _currentInputIndex = _initInputIndex;

            _input = _inputServices[_currentInputIndex];
        }

        private void SetTestInput(Vector3 testDirection)
        {
            var testInput = new TestInputService(_wasdMoveConfig, testDirection);
            SetInput(testInput);
        }

        private void InitPlayer()
        {
            var movement = new MovementRB(_player.gameObject);
            var rotation = new RotationLerp(_player.transform);

            _playerHealth = new SimpleHP(_player.Config.StartHP);

            _player.Init(_input, movement, rotation, _playerHealth);
        }

        private void InitItems()
        {
            _itemsSpawner.Init();

            _itemsService = new ItemsSceneService(_itemsSpawner);

            _collectService = new CollectService();
            _collectService.Init(_player.Config.InitShowedItems);

            _collectController = new CollectController(_itemsService, _collectService);

            _resetables.Add(_itemsSpawner);
            _resetables.Add(_collectService);

            _disposables.Add(_itemsService);
            _disposables.Add(_collectController);
        }

        private void ResetLevel()
        {
            SwitchInput(_initInputIndex);

            foreach (var resetable in _resetables)
            {
                resetable.ResetLevel();
            }
        }

        private void OnDisable()
        {
            _input.Dispose();

            _inputSwitchController.OnInputSwitched -= SwitchInput;

            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        public void SwitchInput()
        {
            _currentInputIndex = (_currentInputIndex + 1) % _inputServices.Length;

            SwitchInput(_currentInputIndex);
        }

        private void SwitchInput(int index)
        {
            _currentInputIndex = index;

            SetInput(_inputServices[_currentInputIndex]);
        }

        private void SetInput(IInputService input)
        {
            _input?.Disable();

            _input = input;

            _input.ResetLevel();

            _player.SetInput(_input);

            _loggerService.LogInputSwitched(_input.GetType().Name);
        }
    }
}