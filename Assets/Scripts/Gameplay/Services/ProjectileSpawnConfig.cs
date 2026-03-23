using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ProjectileSpawnConfig",
        menuName = "Configs/ProjectileSpawn")]
    public sealed class ProjectileSpawnConfig : ScriptableObject
    {
        public Projectile Prefab => _prefab;
        public float Speed => _speed;
        public int Damage => _damage;
        public float CastDistance => _castDistance;
        public int PoolInitCount => _poolInitCount;

        [SerializeField]
        private Projectile _prefab;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private int _damage;

        [SerializeField]
        private float _castDistance = 1f;

        [SerializeField]
        private int _poolInitCount = 10;
    }
}