using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public struct WeaponParams
    {
        public readonly string Name => _name;
        public readonly ProjectileConfig Projectile => _projectileConfig;
        public readonly float ShotPeriod => _shotPeriod;
        public readonly int Capacity => _capacity;

        [SerializeField]
        private string _name;

        [SerializeField]
        private ProjectileConfig _projectileConfig;

        [SerializeField]
        private float _shotPeriod;

        [SerializeField]
        private int _capacity;
    }
}