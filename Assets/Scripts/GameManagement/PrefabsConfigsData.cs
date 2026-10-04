using Gameplay;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "DeviceObjectsConfigs",
        menuName = "Configs/DeviceObjectsConfigs")]
    public class PrefabsConfigsData : ScriptableObject
    {
        [SerializeField]
        private ObjectSODeviceConfig[] _playerMovementConfigs;

        [SerializeField]
        private EnemyDeviceConfigs[] _enemyDeviceConfigs;

        private readonly Dictionary<DeviceType, PlayerMovementConfig> _playerConfigs = new();

        private readonly Dictionary<string, EnemyDeviceConfigs> _enemyNameConfigs = new();

        public void Init()
        {
            foreach (var plConfig in _playerMovementConfigs)
            {
                if (plConfig.Config is not PlayerMovementConfig mvConfig)
                {
                    throw new Exception("config in PlayerMovementConfigs is not a PlayerMovementConfig type");
                }

                _playerConfigs.Add(plConfig.DeviceType, mvConfig);
            }

            foreach (var enemyConfig in _enemyDeviceConfigs)
            {
                enemyConfig.Init();

                _enemyNameConfigs.Add(enemyConfig.Prefab.Name, enemyConfig);
            }
        }

        public PlayerMovementConfig GetPlayerMovementConfig(DeviceType deviceType)
        {
            return _playerConfigs[deviceType];
        }

        public EnemyConfig GetEnemyConfig(string enemyName, DeviceType deviceType)
        {
            return _enemyNameConfigs[enemyName].GetConfig(deviceType);
        }
    }

    [Serializable]
    public struct EnemyDeviceConfigs
    {
        public Enemy Prefab;

        public ObjectSODeviceConfig[] Configs;

        private Dictionary<DeviceType, EnemyConfig> _enemyConfigs;

        public void Init()
        {
            _enemyConfigs = new();

            foreach (var config in Configs)
            {
                if (config.Config is not EnemyConfig enemyConfig)
                {
                    throw new Exception($"config in EnemyConfigs of {config.DeviceType} is not an EnemyConfig type");
                }

                _enemyConfigs.Add(config.DeviceType, enemyConfig);
            }
        }

        public readonly EnemyConfig GetConfig(DeviceType type)
        {
            return _enemyConfigs[type];
        }
    }

    [Serializable]
    public struct ObjectSODeviceConfig
    {
        public DeviceType DeviceType;
        public ScriptableObject Config;
    }
}