using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(HPComponent))]
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

        private void Awake()
        {
            _hp = GetComponent<HPComponent>();

            _hp.Init(_config.HP);
        }

        private void OnEnable()
        {
            _hp.OnHPChanged += HandleDamage;
        }

        private void OnDisable()
        {
            _hp.OnHPChanged -= HandleDamage;
        }

        public void Init(Player player)
        {
            _hp.Init(_config.HP);

            _touchDamage.Init(player.HP, _damageConfig);
        }

        private void HandleDamage(int newHP)
        {
            if (newHP == 0)
            {
                OnKilled?.Invoke(this);

                Debug.Log("killed enemy");
            }
        }
    }
}