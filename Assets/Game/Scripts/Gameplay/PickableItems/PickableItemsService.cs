using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Gameplay
{
    public class PickableItemsService : NetworkBehaviour
    {
        [SerializeField]
        private ItemSpawnPoint _spawnPoint;


        [SerializeField]
        private Transform _pooledItems;

        private PickItemsSpawnConfig _spawnConfig;

        private readonly Dictionary<ItemType, Queue<PickableItem>> _itemPools = new();

        public void Start()
        {
            if (isServer)
            {
                Spawn(ItemType.Medkit);
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            _spawnPoint.OnSpawnRequested += HandleRespawn;
        }

        public override void OnStopServer()
        {
            base.OnStopServer();

            _spawnPoint.OnSpawnRequested -= HandleRespawn;
        }

        public void Init(PickItemsSpawnConfig pickItemsSpawnConfig)
        {
            _spawnConfig = pickItemsSpawnConfig;

            var delay = _spawnConfig.GetSpawnData(_spawnPoint.ItemType).RespawnDelay;

            _spawnPoint.Init(delay);

            foreach (var data in _spawnConfig.ItemsSpawnData)
            {
                NetworkClient.RegisterPrefab(data.Config.PickablePrefab.gameObject);

                _itemPools.Add(data.Config.Type, new Queue<PickableItem>());
            }
        }

        [Server]
        public PickableItem Spawn(ItemType type)
        {
            PickableItem item = Instantiate(_spawnConfig.GetConfig(type).PickablePrefab);

            _spawnPoint.SetItemToPoint(item);

            item.transform.SetParent(transform);

            item.gameObject.SetActive(true);

            NetworkServer.Spawn(item.gameObject);

            item.OnPicked += Unspawn;

            return item;
        }

        [Server]
        public void Unspawn(PickableItem item)
        {
            item.OnPicked -= Unspawn;
            item.gameObject.SetActive(false);
            item.transform.SetParent(_pooledItems, false);
            NetworkServer.Destroy(item.gameObject);
        }

        private void HandleRespawn(ItemType type)
        {
            Spawn(type);
        }
    }
}