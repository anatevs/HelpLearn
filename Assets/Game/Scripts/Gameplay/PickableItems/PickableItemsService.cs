using UnityEngine;
using Mirror;

namespace Gameplay
{
    public class PickableItemsService : NetworkBehaviour
    {
        [SerializeField]
        private ItemSpawnPoint[] _spawnPoints;

        [SerializeField]
        private Transform _pooledItems;

        private PickItemsSpawnConfig _spawnConfig;

        private ItemSpawnPointsStorage _spawnPointsStorage;

        public override void OnStartServer()
        {
            base.OnStartServer();

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
                //point.OnSpawnRequested -= Spawn;
                point.OnSpawnNameRequested -= Spawn;
            }

            _spawnPointsStorage.Dispose();
        }


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
                var prefabGO = data.Config.PickablePrefab.gameObject;

                if (prefabGO.TryGetComponent<NetworkIdentity>(out var identity))
                {
                    if (!NetworkClient.prefabs.ContainsKey(identity.assetId))
                    {
                        NetworkClient.RegisterPrefab(prefabGO);
                    }
                }
            }
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

        private void SpawnItem(ItemConfig itemConfig)
        {
            PickableItem item = Instantiate(itemConfig.PickablePrefab);

            item.Init(itemConfig);

            if (!_spawnPointsStorage.TryGetRandomPoint(itemConfig.Type, out var spawnPoint))
            {
                return;
            }

            spawnPoint.SetItemToPoint(item);

            item.transform.SetParent(transform);

            item.gameObject.SetActive(true);

            NetworkServer.Spawn(item.gameObject);

            item.OnPicked += Unspawn;
        }

        [Server]
        public void Unspawn(PickableItem item)
        {
            item.OnPicked -= Unspawn;
            item.gameObject.SetActive(false);
            item.transform.SetParent(_pooledItems, false);

            NetworkServer.Destroy(item.gameObject);
        }
    }
}