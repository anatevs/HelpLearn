using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(RotationZComponent))]
    [RequireComponent(typeof(ShotComponent))]
    [RequireComponent(typeof(HPComponent))]
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnKilled;
        public MovementComponent Movement => _movement;
        public RotationZComponent Rotation => _rotation;
        public ShotComponent Shot => _shot;

        public EnemyConfig Config => _config;

        [SerializeField]
        private EnemyConfig _config;

        [SerializeField]
        private AttackDetector _attackDetector;

        private MovementComponent _movement;

        private RotationZComponent _rotation;

        private ShotComponent _shot;

        private HPComponent _hp;

        private IEnemyBehaviour _currentBehaviour = null;

        private AttackBehaviour _attackBehaviour;

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
            _hp.OnDamaged += TakeDamage;
        }

        private void OnDisable()
        {
            _attackDetector.OnPlayerDetected -= SetToAttack;
            _hp.OnDamaged -= TakeDamage;
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
            if (_currentBehaviour is AttackBehaviour)
            {
                return;
            }
            SetStrategy(_attackBehaviour);
        }

        private void TakeDamage(int hp)
        {
            if (hp == 0)
            {
                OnKilled?.Invoke(this);
            }
        }
    }
}