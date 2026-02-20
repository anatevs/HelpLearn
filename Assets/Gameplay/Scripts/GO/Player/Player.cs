using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(RotationZComponent))]
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(ShotComponent))]
    [RequireComponent(typeof(HPComponent))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private InputHandler _input;

        [SerializeField]
        private GameConfig _config;

        private RotationZComponent _rotation;
        private MovementComponent _movement;
        private ShotComponent _shot;
        private HPComponent _hp;

        private void Awake()
        {
            _rotation = GetComponent<RotationZComponent>();
            _movement = GetComponent<MovementComponent>();
            _shot = GetComponent<ShotComponent>();
            _hp = GetComponent<HPComponent>();

            _hp.Init(_config.HP);
        }

        private void OnEnable()
        {
            _input.OnShoot += Shoot;
        }

        private void OnDisable()
        {
            _input.OnShoot -= Shoot;
        }

        private void Update()
        {
            var direction = _input.Position - transform.position;

            _rotation.RotateUpdate(direction, _config.Movement.RotationSpeed);

            _movement.MoveUpdate(_input.MoveDirection, _config.Movement.MovementSpeed);
        }

        private void Shoot()
        {
            _shot.Shoot(transform.up);
        }
    }
}