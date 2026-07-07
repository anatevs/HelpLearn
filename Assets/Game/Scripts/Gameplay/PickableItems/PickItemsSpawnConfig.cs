using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PickItemsSpawnConfig",
        menuName = "Configs/Items/PickItemsSpawn")]
    public class PickItemsSpawnConfig : ScriptableObject
    {
        public ItemSpawnData[] ItemsSpawnData => _itemsSpawnData;

        public ItemType[] Types => _dataDict.Keys.ToArray();

        [SerializeField]
        private ItemSpawnData[] _itemsSpawnData;

        private readonly Dictionary<ItemType, ItemSpawnData> _dataDict = new();

        private readonly Dictionary<ItemType, List<ItemSpawnData>> _itemsGroupData = new();

        private readonly Dictionary<string, ItemSpawnData> _itemNameData = new();

        public void Init()
        {
            foreach (var data in _itemsSpawnData)
            {
                var type = data.Config.Type;

                _dataDict.TryAdd(type, data);

                if (!_itemsGroupData.ContainsKey(type))
                {
                    _itemsGroupData.Add(type, new List<ItemSpawnData>());
                }

                _itemsGroupData[type].Add(data);

                var itemName = data.Config.Name;
                if (!_itemNameData.TryAdd(itemName, data))
                {
                    Debug.LogWarning($"two item configs with same Name {itemName}!!");
                }
            }
        }

        public ItemConfig GetConfig(ItemType type)
        {
            if (!_dataDict.ContainsKey(type))
            {
                return null;
            }

            return _dataDict[type].Config;
        }

        public ItemConfig GetConfig(string itemName)
        {
            if (!_itemNameData.ContainsKey(itemName))
            {
                return null;
            }

            return _itemNameData[itemName].Config;
        }

        public ItemSpawnData GetSpawnData(ItemType type)
        {
            if (!_dataDict.ContainsKey(type))
            {
                return default;
            }

            return _dataDict[type];
        }

        public ItemSpawnData GetSpawnData(string itemName)
        {
            if (!_itemNameData.ContainsKey(itemName))
            {
                return default;
            }

            return _itemNameData[itemName];
        }

        public List<ItemSpawnData> GetGroupData(ItemType type)
        {
            if (!_itemsGroupData.ContainsKey(type))
            {
                return null;
            }

            return _itemsGroupData[type];
        }
    }

    [Serializable]
    public struct ItemSpawnData
    {
        [SerializeField]
        public ItemConfig Config;

        [SerializeField]
        public int InitCount;

        [SerializeField]
        public float RespawnDelay;

        [SerializeField]
        public float SpawnWeightRate;
    }
}