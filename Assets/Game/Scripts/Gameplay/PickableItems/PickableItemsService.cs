using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Gameplay
{
    public class PickableItemsService : MonoBehaviour
    {
        private GameItemsConfig _itemsConfig;

        private Dictionary<ItemType, Queue<PickableItem>> _itemPools = new();

        public void Start()
        {
            foreach (var config in _itemsConfig.Configs)
            {
                NetworkClient.RegisterPrefab(config.PickablePrefab.gameObject);

                _itemPools.Add(config.Type, new Queue<PickableItem>());
            }
        }

        public void Init(GameItemsConfig itemsConfig)
        {
            _itemsConfig = itemsConfig;
        }

        public void Spawn(ItemType type)
        {

        }

        public void Unspawn()
        {
            //item.Pick();
            //Destroy(item.gameObject);
        }
    }
}