using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(ShotComponent))]
    public class Enemy : MonoBehaviour
    {
        public MovementComponent Movement => _movement;
        public ShotComponent Shot => _shot;

        public EnemyConfig Config => _config;

        [SerializeField]
        private EnemyConfig _config;

        [SerializeField]
        private AttackDetector _attackDetector;

        private MovementComponent _movement;

        private ShotComponent _shot;

        private IEnemyBehaviour _currentBehaviour = null;

        private AttackBehaviour _attackBehaviour;

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();

            _shot = GetComponent<ShotComponent>();

            _attackDetector.Init();

            _attackDetector.SetDetectDistance(_config.DetectDistance);
        }

        private void OnEnable()
        {
            _attackDetector.OnPlayerDetected += SetToAttack;
        }

        private void OnDisable()
        {
            _attackDetector.OnPlayerDetected -= SetToAttack;
        }

        private void Update()
        {
            _currentBehaviour?.ActUpdate();
        }

        public void Init(AttackBehaviour attackBehaviour)//, ProjectileSpawnService projectileSpawn)
        {
            _attackBehaviour ??= attackBehaviour;
            //_shot.Init(projectileSpawn);
        }

        public void SetStrategy(EnemyBehaviour behaviour)
        {
            _currentBehaviour = behaviour;
        }

        private void SetToAttack()
        {
            if (_currentBehaviour is AttackBehaviour)
            {
                return;
            }
            SetStrategy(_attackBehaviour);
        }
    }
}