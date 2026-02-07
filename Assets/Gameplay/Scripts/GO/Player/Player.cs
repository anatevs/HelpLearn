using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(RotationZComponent))]
    [RequireComponent(typeof(MovementComponent))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private InputHandler _input;

        [SerializeField]
        private GameConfig _playerConfig;

        private RotationZComponent _rotation;
        private MovementComponent _movement;

        private void Awake()
        {
            _rotation = GetComponent<RotationZComponent>();
            _movement = GetComponent<MovementComponent>();
        }

        private void Update()
        {
            _rotation.RotateUpdate(_input.Position, _playerConfig.Movement.RotationSpeed);

            _movement.MoveUpdate(_input.MoveDirection, _playerConfig.Movement.MovementSpeed);
        }
    }
}