using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    public class Enemy : MonoBehaviour
    {
        public MovementComponent Movement => _movement;

        public EnemyConfig Config => _config;

        [SerializeField]
        private EnemyConfig _config;

        [SerializeField]
        private AttackDetector _attackDetector;

        private MovementComponent _movement;

        private IEnemyBehaviour _currentBehaviour = null;

        private AttackBehaviour _attackBehaviour;

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();

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

        public void Init(AttackBehaviour attackBehaviour)
        {
            _attackBehaviour ??= attackBehaviour;
        }

        public void SetStrategy(EnemyBehaviour behaviour)
        {
            _currentBehaviour = behaviour;
        }

        private void SetToAttack()
        {
            SetStrategy(_attackBehaviour);
        }
    }
}