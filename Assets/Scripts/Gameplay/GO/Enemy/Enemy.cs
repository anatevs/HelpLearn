using System;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    [RequireComponent(typeof(HPComponent))]
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnKilled;

        public EnemyConfig Config => _config;

        [SerializeField]
        private EnemyConfig _config;

        [SerializeField]
        private TouchDamageConfig _damageConfig;

        [SerializeField]
        private TouchDamage _touchDamage;

        private HPComponent _hp;
        private NavMeshAgent _agent;
        private Player _player;

        private void Awake()
        {
            _hp = GetComponent<HPComponent>();

            _hp.Init(_config.HP);

            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            _hp.OnHPChanged += HandleDamage;
        }

        private void OnDisable()
        {
            _hp.OnHPChanged -= HandleDamage;
        }

        private void Update()
        {
            _agent.SetDestination(_player.transform.position);
        }

        public void Init(Player player)
        {
            _player = player;

            _hp.Init(_config.HP);

            _touchDamage.Init(player.HP, _damageConfig);

            _agent.speed = _config.FollowSpeed;
            _agent.stoppingDistance = _config.StopDistance;
        }

        private void HandleDamage(int newHP)
        {
            if (newHP == 0)
            {
                OnKilled?.Invoke(this);
            }
        }
    }
}