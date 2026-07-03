using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ItemsListConfig",
        menuName = "Configs/Items/ItemsList")]
    public class GameItemsConfig : ScriptableObject
    {
        public ItemConfig[] Configs => _configs;

        [SerializeField]
        private ItemConfig[] _configs;

        private Dictionary<ItemType, ItemConfig> _configsDict = new();

        public void Init()
        {
            foreach (var config in _configs)
            {
                _configsDict.TryAdd(config.Type, config);
            }
        }

        public ItemConfig GetConfig(ItemType type)
        {
            if (!_configsDict.ContainsKey(type))
            {
                return null;
            }

            return _configsDict[type];
        }
    }
}