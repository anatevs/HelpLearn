using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class InventoryStorage
    {
        public event Action<ItemType, int> OnItemUpdated;

        private readonly Dictionary<ItemType, int> _items = new();

        public void AddItem(ItemType type)
        {
            if (!_items.ContainsKey(type))
            {
                _items.Add(type, 0);
            }

            _items[type]++;

            OnItemUpdated?.Invoke(type, _items[type]);
        }

        public bool TryTakeItem(ItemType typeName)
        {
            if (!_items.ContainsKey(typeName) || _items[typeName] <= 0)
            {
                return false;
            }

            _items[typeName]--;

            OnItemUpdated?.Invoke(typeName, _items[typeName]);

            return true;
        }

        public int GetAmount(ItemType typeName)
        {
            if (!_items.ContainsKey(typeName))
            {
                return 0;
            }

            return _items[typeName];
        }
    }
}