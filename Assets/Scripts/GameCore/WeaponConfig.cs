using System;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "WeaponConfig",
        menuName = "Configs/Weapon")]
    public class WeaponConfig : ScriptableObject
    {
        public float ProjectileSpeed => _projectileSpeed;
        public float FireCooldown => _fireCooldown;
        public float Damage => _damage;

        [SerializeField]
        private float _projectileSpeed;

        [SerializeField]
        private float _fireCooldown;

        [SerializeField]
        private float _damage;
    }
}