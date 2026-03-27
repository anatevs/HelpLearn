using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ProjectileSpawnConfig",
        menuName = "Configs/ProjectileSpawn")]
    public sealed class ProjectileConfig : ScriptableObject
    {
        public Projectile Prefab => _prefab;
        public int Damage => _damage;
        public float Speed => _speed;
        public float CastDistance => _castDistance;
        public float DamageRadius => _damageRadius;
        public int PoolInitCount => _poolInitCount;

        [SerializeField]
        private Projectile _prefab;

        [SerializeField]
        private int _damage;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _castDistance = 1f;

        [SerializeField]
        private float _damageRadius;

        [SerializeField]
        private int _poolInitCount = 10;
    }
}