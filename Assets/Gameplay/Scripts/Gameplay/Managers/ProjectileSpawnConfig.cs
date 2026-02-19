using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ProjectileSpawnConfig",
        menuName = "Configs/ProjectileSpawn")]
    public class ProjectileSpawnConfig : ScriptableObject
    {
        public Projectile Prefab => _prefab;
        public float Speed => _speed;
        public int Damage => _damage;
        public int PoolInitCount => _poolInitCount;

        [SerializeField]
        private Projectile _prefab;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private int _damage;

        [SerializeField]
        private int _poolInitCount = 10;
    }
}