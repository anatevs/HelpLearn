using UnityEngine;
using Mirror;
using System.Collections.Generic;

namespace Gameplay
{
    public class PickableItemsService : NetworkBehaviour
    {
        [SerializeField]
        private ItemSpawnPoint[] _spawnPoints;

        [SerializeField]
        private Transform _poolTransform;

        private PickItemsSpawnConfig _spawnConfig;

        private ItemSpawnPointsStorage _spawnPointsStorage;

        private uint _spawnCount = 0;
        private readonly Dictionary<uint, PickableItem> _activeItems = new();

        private readonly Dictionary<ItemConfig, PickableItemPool> _pickItemPools = new();

        public void Init(PickItemsSpawnConfig pickItemsSpawnConfig)
        {
            _spawnConfig = pickItemsSpawnConfig;

            foreach (var point in _spawnPoints)
            {
                var groupList = _spawnConfig.GetGroupData(point.ItemType);

                point.Init(groupList);

                point.OnSpawnNameRequested += Spawn;
            }

            foreach (var data in _spawnConfig.ItemsSpawnData)
            {
                var pool = new PickableItemPool(data.Config.PickablePrefab,
                    _spawnConfig.InitPoolSize, _poolTransform, transform);

                pool.InitItemConfig(data.Config);

                _pickItemPools.TryAdd(data.Config, pool);
            }
        }

        private void OnDestroy()
        {
            foreach (var pool in _pickItemPools.Values)
            {
                pool.Dispose();
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            foreach (var pool in _pickItemPools.Values)
            {
                pool.ServerPopulatePool();
            }

            _spawnPointsStorage = new ItemSpawnPointsStorage(_spawnPoints);

            foreach (var type in _spawnConfig.Types)
            {
                var initCount = _spawnConfig.GetSpawnData(type).InitCount;

                for (int i = 0; i < initCount; i++)
                {
                    Spawn(type);
                }
            }
        }

        public override void OnStopServer()
        {
            base.OnStopServer();

            foreach (var point in _spawnPoints)
            {
                point.OnSpawnNameRequested -= Spawn;
            }

            _spawnPointsStorage.Dispose();
        }

        [Server]
        public bool TryGetSpawnedItem(uint spawnId, out PickableItem spawnedItem)
        {
            if (!_activeItems.TryGetValue(spawnId, out spawnedItem))
            {
                Debug.Log($"can't get item with spawn id {spawnId} from active items");

                return false;
            }

            return true;
        }

        [Server]
        private void Spawn(ItemType type)
        {
            var itemConfig = _spawnConfig.GetConfig(type);

            SpawnItem(itemConfig);
        }

        [Server]
        public void Spawn(string itemName)
        {
            var itemConfig = _spawnConfig.GetConfig(itemName);

            SpawnItem(itemConfig);
        }

        [Server]
        private void SpawnItem(ItemConfig itemConfig)
        {
            if (!_spawnPointsStorage.TryGetRandomPoint(itemConfig.Type, out var spawnPoint))
            {
                return;
            }

            var spawnTransform = spawnPoint.transform;

            var item = _pickItemPools[itemConfig].Spawn();

            spawnPoint.SetItemToPoint(item);

            NetworkServer.Spawn(item.gameObject);

            item.OnPicked += Unspawn;

            item.SpawnId = _spawnCount;
            _activeItems.Add(_spawnCount, item);
            _spawnCount++;
        }

        [Server]
        public void Unspawn(PickableItem item)
        {
            item.OnPicked -= Unspawn;

            _activeItems.Remove(item.SpawnId);

            NetworkServer.UnSpawn(item.gameObject);
        }
    }
}