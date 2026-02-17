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

        private Transform[] _patrolPoints;

        private Player _player;

        private MovementComponent _movement;

        private IEnemyBehaviour _currentBehaviour = null;

        private AttackBehaviour _attackBehaviour;

        private bool _isAttacking = false;

        private void Awake()
        {
            _config.Init();

            _movement = GetComponent<MovementComponent>();
        }

        private void Update()
        {
            _currentBehaviour?.ActUpdate();

            if (!_isAttacking && _player != null && (_player.transform.position - transform.position).sqrMagnitude < _config.DetectSqrDistance)
            {
                SetStrategy(_attackBehaviour);
            }
        }

        public void Init(Transform[] patrolPoints, Player player)
        {
            _patrolPoints = patrolPoints;
            _player = player;

            var startBehaviour = new PatrolBehaviour(this, _patrolPoints);
            SetStrategy(startBehaviour);

            _attackBehaviour ??= new AttackBehaviour(this, _player.transform);
        }

        public void SetStrategy(EnemyBehaviour behaviour)
        {
            _currentBehaviour = behaviour;

            _isAttacking = (behaviour is AttackBehaviour);
        }
    }
}