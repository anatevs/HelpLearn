using Mirror;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "WeaponsConfig",
        menuName = "Configs/Weapons")]
    public class GameWeaponsConfig : ScriptableObject
    {
        [SerializeField]
        private WeaponConfig[] _weaponConfigs;

        private readonly Dictionary<string, WeaponConfig> _weaponConfigsDict = new();

        private void OnValidate()
        {
            _weaponConfigs = _weaponConfigs
                .DistinctBy(x => x.Name)
                .ToArray();
        }

        public void Init()
        {
            foreach (var weaponConfig in _weaponConfigs)
            {
                _weaponConfigsDict.TryAdd(weaponConfig.Name, weaponConfig);
            }
        }

        //public void RegisterTracePrefabs()
        //{
        //    foreach (var weaponConfig in _weaponConfigs)
        //    {
        //        NetworkClient.RegisterPrefab(weaponConfig.TracerPrefab.gameObject);
        //    }
        //}

        public WeaponConfig GetConfig(string name)
        {
            return _weaponConfigsDict[name];
        }
    }
}