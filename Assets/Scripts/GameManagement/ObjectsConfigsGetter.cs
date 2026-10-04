using Gameplay;
using UnityEngine;

namespace GameManagement
{
    public class ObjectsConfigsGetter
    {
        private readonly DeviceType _deviceType;

        private readonly PrefabsConfigsData _objectsConfigsData;

        public ObjectsConfigsGetter(DeviceType deviceType,
            PrefabsConfigsData objectsConfigsData)
        {
            _deviceType = deviceType;
            _objectsConfigsData = objectsConfigsData;
        }

        public PlayerMovementConfig GetPlayerConfig()
        {
            return _objectsConfigsData.GetPlayerMovementConfig(_deviceType);
        }

        public EnemyConfig GetEnemyConfig(string enemyName)
        {
            return _objectsConfigsData.GetEnemyConfig(enemyName, _deviceType);
        }
    }
}