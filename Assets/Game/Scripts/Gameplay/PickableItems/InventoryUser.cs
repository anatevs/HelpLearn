using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class InventoryUser : MonoBehaviour
    {
        private InventoryStorage _inventoryStorage;
        private PickItemsSpawnConfig _pickItemsConfig;

        private readonly Dictionary<ItemType, bool> _notCooldownes = new();


        public void Init(InventoryStorage inventoryStorage, PickItemsSpawnConfig pickItemsConfig)
        {
            _inventoryStorage = inventoryStorage;

            _pickItemsConfig = pickItemsConfig;

            foreach (var type in _pickItemsConfig.Types)
            {
                _notCooldownes.TryAdd(type, true);
            }
        }

        public void UseItem(string itemName, bool extraCheck, Action<ItemConfig> useAction)
        {
            var config = _pickItemsConfig.GetConfig(itemName);

            if (_inventoryStorage.TryTakeItem(itemName))
            {
                if (_notCooldownes[config.Type] && extraCheck)
                {
                    useAction.Invoke(config);

                    StartCoroutine(WaitItemUsing(config.UseWait, config.Type));
                }
                else
                {
                    _inventoryStorage.AddItem(itemName);
                }
            }
        }

        private IEnumerator WaitItemUsing(WaitForSeconds wait, ItemType itemType)
        {
            _notCooldownes[itemType] = false;

            yield return wait;

            _notCooldownes[itemType] = true;
        }
    }
}