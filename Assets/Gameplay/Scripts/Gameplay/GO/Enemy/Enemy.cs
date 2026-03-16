using EventBusNamespace;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(RotationZComponent))]
    [RequireComponent(typeof(ShotComponent))]
    [RequireComponent(typeof(HPComponent))]
    public sealed class Enemy : MonoBehaviour
    {
        public MovementComponent Movement => _movement;
        public RotationZComponent Rotation => _rotation;
        public ShotComponent Shot => _shot;
        public EnemyConfig Config => _config;
        public string StrategyName => _currentStrategy?.GetType().Name;

        [SerializeField]
        private EnemyConfig _config;

        [SerializeField]
        private AttackDetector _attackDetector;

        [SerializeField]
        private EventBus _eventBus;

        private MovementComponent _movement;

        private RotationZComponent _rotation;

        private ShotComponent _shot;

        private HPComponent _hp;

        private IEnemyStrategy _currentStrategy = null;

        private AttackStrategy _attackStrategy;

        private ProjectileSpawnService _projectileService;

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();

            _rotation = GetComponent<RotationZComponent>();

            _shot = GetComponent<ShotComponent>();

            _hp = GetComponent<HPComponent>();

            _hp.Init(_config.HP);

            _attackDetector.Init();

            _attackDetector.SetDetectDistance(_config.DetectDistance);
        }

        private void OnEnable()
        {
            _attackDetector.OnPlayerDetected += SetToAttack;
            _hp.OnDamaged += HandleDamage;
        }

        private void OnDisable()
        {
            _attackDetector.OnPlayerDetected -= SetToAttack;
            _hp.OnDamaged -= HandleDamage;
        }

        private void Update()
        {
            _currentStrategy?.ActUpdate();
        }

        public void Init(AttackStrategy attackBehaviour, ProjectileSpawnService projectileService)
        {
            _hp.Init(_config.HP);
            _attackStrategy = attackBehaviour;

            _projectileService = projectileService;
            _shot.Init(_projectileService);
        }

        public void SetStrategy(EnemyStrategy behaviour)
        {
            _currentStrategy = behaviour;
        }

        private void SetToAttack()
        {
            if (_currentStrategy is AttackStrategy)
            {
                return;
            }
            SetStrategy(_attackStrategy);
        }

        private void HandleDamage(int damage)
        {
            _eventBus.RaiseEvent(new EnemyDamageEvent((this, damage)));

            if (_hp.HP == 0)
            {
                _eventBus.RaiseEvent(new EnemyKilledEvent(this));
            }
        }
    }
}