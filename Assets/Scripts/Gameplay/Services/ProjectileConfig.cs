using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ProjectileConfig",
        menuName = "Configs/Projectile")]
    public sealed class ProjectileConfig : ScriptableObject
    {
        public Projectile Prefab => _prefab;
        public int Damage => _damage;
        public float Speed => _speed;
        public float DamageRadius => _damageRadius;

        [SerializeField]
        private Projectile _prefab;

        [SerializeField]
        private int _damage;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _damageRadius;
    }
}