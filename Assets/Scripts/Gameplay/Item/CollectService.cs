using System;
using System.Collections.Generic;

namespace Gameplay
{
    public sealed class CollectService : 
        ICollectService
    {
        public event Action<string, int> OnChanged;

        public event Action<string, int> OnNewAdded;

        public event Action<string> OnRemoved;

        private readonly Dictionary<string, int> _collectedItems = new();

        private ItemConfig[] _initItemConfigs;

        public void Init(ItemConfig[] initItemsConfigs)
        {
            _initItemConfigs = initItemsConfigs;
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
            OnNewAdded?.Invoke(name, _collectedItems[name]);
        }

        public bool TryTakeItem(string name, int amount)
        {
            if (_collectedItems[name] >= amount)
            {
                _collectedItems[name] -= amount;

                OnChanged?.Invoke(name, _collectedItems[name]);

                return true;
            }

            return false;
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

            _collectedItems.Clear();

            for (int i = 0; i < _initItemConfigs.Length; i++)
            {
                AddNewItem(_initItemConfigs[i], 0);
            }
        }
    }
}