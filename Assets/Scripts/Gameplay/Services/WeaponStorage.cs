using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class WeaponStorage : MonoBehaviour
    {
        public event Action<Weapon> OnWeaponChanged;

        [SerializeField]
        private WeaponConfig[] _configs;

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

            ChangeWeapon(_configs[0].Name);
        }

        public void Add(WeaponConfig config)
        {
            var weapon = config.CreateNewWeapon();

            _weapons.Add(config.Name, weapon);

            SetToInactive(weapon);

            weapon.Construct(_projectileSpawn);
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
            weapon.gameObject.SetActive(false);
            weapon.transform.parent = transform;
        }
    }
}