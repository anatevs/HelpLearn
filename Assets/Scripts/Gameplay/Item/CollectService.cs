using System;
using System.Collections.Generic;

namespace Gameplay
{
    public sealed class CollectService : 
        ICollectService
    {
        public event Action<string, int> OnChanged;

        public event Action<ItemConfig, int> OnNewAdded;

        public event Action<string> OnRemoved;

        public IReadOnlyList<string> Names => _names;

        private readonly Dictionary<string, int> _collectedItems = new();

        private readonly Dictionary<string, ItemConfig> _configs = new();

        private readonly List<string> _names = new();

        private readonly ItemConfig[] _initItemConfigs;

        public CollectService(ItemConfig[] initItemsConfigs)
        {
            _initItemConfigs = initItemsConfigs;

            InitItems();
        }

        public void ResetLevel()
        {
            if (_collectedItems.Count > 0)
            {
                foreach (var name in _collectedItems.Keys)
                {
                    OnRemoved?.Invoke(name);
                }
            }

            InitItems();
        }

        public ItemConfig GetItemConfig(string name)
        {
            return _configs[name];
        }

        public int GetAmount(string name)
        {
            return _collectedItems[name];
        }

        public void AddItem(ItemConfig itemConfig)
        {
            var name = itemConfig.Name;

            if (_collectedItems.ContainsKey(name))
            {
                _collectedItems[name] += itemConfig.Amount;
            }
            else
            {
                AddNewItem(itemConfig, itemConfig.Amount);
            }

            OnChanged?.Invoke(name, _collectedItems[name]);
        }

        public void AddNewItem(ItemConfig config, int amount)
        {
            var name = config.Name;
            _collectedItems.Add(name, amount);
            OnNewAdded?.Invoke(config, _collectedItems[name]);

            _configs.TryAdd(name, config);
            _names.Add(name);
        }

        public bool TryTakeItem(string name, int amount)
        {
            if (_collectedItems.TryGetValue(name, out var value) && value >= amount)
            {
                value -= amount;

                OnChanged?.Invoke(name, value);

                return true;
            }

            return false;
        }

        private void InitItems()
        {
            _collectedItems.Clear();

            for (int i = 0; i < _initItemConfigs.Length; i++)
            {
                AddNewItem(_initItemConfigs[i], 0);
            }
        }
    }
}