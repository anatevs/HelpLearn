using Input;
using UnityEngine;
using GameManagement;


namespace Gameplay
{
    public sealed class PlayerController : MonoBehaviour,
        IDamagable,
        IResetable
    {
        public PlayerConfig Config => _config;
        public IHealth Health => _health;

        private Vector3 _startPosition;
        private Quaternion _startRotation;

        [SerializeField]
        private PlayerConfig _config;

        private IInputService _input;

        private IMovementService _movement;

        private IRotationService _rotation;

        private IHealth _health;

        public void Init(IInputService input,
            IMovementService movement,
            IRotationService rotation,
            IHealth health)
        {
            _input = input;
            _movement = movement;
            _rotation = rotation;
            _health = health;
        }

        public void SetInput(IInputService newInput)
        {
            _input = newInput;
        }

        public void ResetLevel()
        {
            _movement.ResetLevel();
            transform.SetPositionAndRotation(_startPosition, _startRotation);

            _health.ResetLevel();
        }

        private void Awake()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        private void Update()
        {
            _input.Update(transform);
            _movement.MoveUpdate(_input.Move, _input.Speed);
            _rotation.RotateUpdate(_input.LookDirection, _input.RotationSpeed);
        }

        private void FixedUpdate()
        {
            _movement.MoveFixedUpdate(_input.Move, _input.Speed);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IItem>(out var item))
            {
                item.Collect();
            }
        }
    }
}