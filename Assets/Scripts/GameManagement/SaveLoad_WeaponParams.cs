using GameCore;
using System;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "SaveLoad_Weapon",
        menuName = "Configs/SaveLoad/Weapon")]
    public class SaveLoad_WeaponParams : SaveLoadSO<WeaponData>
    {
        [SerializeField]
        private WeaponConfig _weaponConfig;

        public float Cooldown
        {
            get => _currentData.Cooldown;
            set => _currentData.Cooldown = value;
        }

        public float Speed
        {
            get => _currentData.Speed;
            set => _currentData.Speed = value;
        }

        public override void Load()
        {
            base.Load();

            if (!(HasLoadedData()))
            {
                if (_weaponConfig == null)
                {
                    Debug.LogError($"No weapon config assigned in {this.name} component");
                }

                _currentData = new WeaponData(_weaponConfig);
            }
        }
    }

    [Serializable]
    public struct WeaponData
    {
        public float Cooldown;

        public float Speed;

        public WeaponData(float cooldown, float speed)
        {
            Cooldown = cooldown;
            Speed = speed;
        }

        public WeaponData(WeaponConfig weaponConfig)
        {
            Cooldown = weaponConfig.FireCooldown;
            Speed = weaponConfig.ProjectileSpeed;
        }
    }
}