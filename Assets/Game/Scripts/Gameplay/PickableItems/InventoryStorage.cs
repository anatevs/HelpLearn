using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class InventoryStorage
    {
        public event Action<ItemType, int> OnItemUpdated;

        public event Action<string, int> OnItemNamedUpdated;

        public event Action<string> OnEmptyAccessed;

        private readonly Dictionary<ItemType, int> _items = new();

        private readonly Dictionary<string, int> _itemsNamed = new();

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
                OnEmptyAccessed?.Invoke(typeName.ToString());
                return false;
            }

            _items[typeName]--;

            OnItemUpdated?.Invoke(typeName, _items[typeName]);

            return true;
        }

        public int GetAmount(ItemType itemType)
        {
            if (!_items.ContainsKey(itemType))
            {
                return 0;
            }

            return _items[itemType];
        }


        public void AddItem(string itemName)
        {
            if (!_itemsNamed.ContainsKey(itemName))
            {
                _itemsNamed.Add(itemName, 0);
            }

            _itemsNamed[itemName]++;

            OnItemNamedUpdated?.Invoke(itemName, _itemsNamed[itemName]);
        }

        public bool TryTakeItem(string itemName)
        {
            if (!_itemsNamed.ContainsKey(itemName) || _itemsNamed[itemName] <= 0)
            {
                OnEmptyAccessed?.Invoke(itemName);
                return false;
            }

            _itemsNamed[itemName]--;

            OnItemNamedUpdated?.Invoke(itemName, _itemsNamed[itemName]);

            return true;
        }

        public int GetAmount(string itemName)
        {
            if (!_itemsNamed.ContainsKey(itemName))
            {
                return 0;
            }

            return _itemsNamed[itemName];
        }
    }
}