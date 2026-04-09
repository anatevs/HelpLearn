using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class WeaponStorage : MonoBehaviour
    {
        public event Action<Weapon> OnWeaponChanged;
        public event Action<Weapon, int> OnWeaponAdded;

        public int FirstActiveIndex => _firstActiveIndex;

        [SerializeField]
        private WeaponConfig[] _configs;

        [SerializeField]
        private int _firstActiveIndex = 0;

        private ProjectileSpawnService _projectileSpawn;

        private readonly Dictionary<string, Weapon> _weapons = new();

        private Weapon _currentActive;

        public void Construct(ProjectileSpawnService projectileSpawn)
        {
            _projectileSpawn = projectileSpawn;
        }

        public void Init()
        {
            foreach (var config in _configs)
            {
                Add(config);
            }

            ChangeWeapon(_configs[_firstActiveIndex].Name);
        }

        public void ResetLevel()
        {
            foreach (var weapon in _weapons.Values)
            {
                weapon.ResetLevel();
            }

            ChangeWeapon(_configs[_firstActiveIndex].Name);
        }

        public void Add(WeaponConfig config)
        {
            var weapon = config.CreateNewWeapon();

            _weapons.Add(config.Name, weapon);

            SetToInactive(weapon);

            weapon.Construct(_projectileSpawn);

            OnWeaponAdded?.Invoke(weapon, config.Capacity);
        }

        public void ChangeWeapon(string name)
        {
            if (_currentActive != null)
            {
                SetToInactive(_currentActive);
            }

            _currentActive = _weapons[name];
            OnWeaponChanged?.Invoke(_currentActive);
        }

        private void SetToInactive(Weapon weapon)
        {
            weapon.ChangeActive(false);
            weapon.transform.parent = transform;
        }
    }
}