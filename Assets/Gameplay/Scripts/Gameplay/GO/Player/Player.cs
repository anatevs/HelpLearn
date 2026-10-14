using EventBusNamespace;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(RotationZComponent))]
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(ShotComponent))]
    [RequireComponent(typeof(HPComponent))]
    public sealed class Player : MonoBehaviour
    {
        public GameConfig Config => _config;
        public HPComponent HPComponent => _hp;

        [SerializeField]
        private InputHandler _input;

        [SerializeField]
        private GameConfig _config;

        private EventBus _eventBus;

        private RotationZComponent _rotation;
        private MovementComponent _movement;
        private ShotComponent _shot;
        private HPComponent _hp;

        private Vector3 _initPos;

        private ProjectileSpawnService _projectileService;

        public void Construct(ProjectileSpawnService projectileService,
            EventBus eventBus)
        {
            _projectileService = projectileService;

            _shot = GetComponent<ShotComponent>();
            _shot.Init(_projectileService);

            _eventBus = eventBus;

            _input.Construct(_eventBus);
        }

        private void Awake()
        {
            _rotation = GetComponent<RotationZComponent>();
            _movement = GetComponent<MovementComponent>();

            _hp = GetComponent<HPComponent>();
            _hp.Init(_config.HP);

            _initPos = transform.position;
        }

        private void OnEnable()
        {
            _input.OnShoot += Shoot;
            _hp.OnDamaged += HandleDamage;
        }

        private void OnDisable()
        {
            _input.OnShoot -= Shoot;
            _hp.OnDamaged -= HandleDamage;
        }

        private void Update()
        {
            var direction = _input.Position - transform.position;

            _rotation.RotateUpdate(direction, _config.Movement.RotationSpeed);

            _movement.MoveUpdate(_input.MoveDirection, _config.Movement.MovementSpeed);
        }

        public void Reset()
        {
            transform.position = _initPos;
        }

        private void Shoot()
        {
            _shot.Shoot(transform.up);
        }

        private void HandleDamage(int damage)
        {
            _eventBus.RaiseEvent(new PlayerDamageEvent((damage, _hp.HP)));

            if (_hp.HP <= 0)
            {
                _eventBus.RaiseEvent(new GameLoseEvent());
            }
        }
    }
}