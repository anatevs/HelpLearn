using EventBusNamespace;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class InputHandler : MonoBehaviour
    {
        public event Action OnShoot;

        public Vector3 Position => _pointerPosition;

        public Vector3 MoveDirection => _moveDirection;

        private InputSystem_Actions _actions;

        private InputSystem_Actions.PlayerActions _playerActions;

        private Camera _camera;

        private Vector3 _pointerPosition;

        private Vector3 _moveDirection;

        private bool _isOverUI = false;

        private void Awake()
        {
            _actions = new();

            _playerActions = _actions.Player;

            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _actions.Enable();

            _actions.Player.Move.performed += Move;
            _actions.Player.Move.canceled += StopMoving;

            _actions.Player.Attack.performed += Shot;

            SubscribeEventBus();
        }

        private void OnDisable()
        {
            _actions.Player.Move.performed -= Move;
            _actions.Player.Move.canceled -= StopMoving;

            _actions.Player.Attack.performed -= Shot;

            UnsubcribeEventBus();

            _actions.Disable();
        }

        private void Update()
        {
            var screenPos = _playerActions.PointerPosition.ReadValue<Vector2>();

            _pointerPosition = _camera.ScreenToWorldPoint(screenPos);

            _moveDirection = _playerActions.Move.ReadValue<Vector2>();

            _isOverUI = EventSystem.current.IsPointerOverGameObject();
        }

        private void Move(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>().normalized;
        }

        private void StopMoving(InputAction.CallbackContext context)
        {
            _moveDirection = Vector3.zero;
        }

        private void Shot(InputAction.CallbackContext context)
        {
            if (!_isOverUI)
            {
                OnShoot?.Invoke();
            }
        }

        private void SubscribeEventBus()
        {
            EventBus.Subscribe<GamePlayingEvent>(SetPlaying);
            EventBus.Subscribe<GameInitEvent>(SetUnplaying);
            EventBus.Subscribe<GamePausedEvent>(SetUnplaying);
            EventBus.Subscribe<GameWinEvent>(SetUnplaying);
            EventBus.Subscribe<GameLoseEvent>(SetUnplaying);
        }

        private void UnsubcribeEventBus()
        {
            EventBus.Unsubscribe<GamePlayingEvent>(SetPlaying);
            EventBus.Unsubscribe<GameInitEvent>(SetUnplaying);
            EventBus.Unsubscribe<GamePausedEvent>(SetUnplaying);
            EventBus.Unsubscribe<GameWinEvent>(SetUnplaying);
            EventBus.Unsubscribe<GameLoseEvent>(SetUnplaying);
        }


        private void SetPlaying(GamePlayingEvent _)
        {
            _playerActions.Enable();
        }

        private void SetUnplaying<T>(T e) where T : ChangeStateEvent
        {
            _playerActions.Disable();
        }
    }
}