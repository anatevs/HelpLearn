using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ClientPool<T> : IDisposable where T : NetworkBehaviour
    {
        protected T _prefab;

        private readonly int _initSize;

        private readonly Transform _poolTransform;

        private readonly Transform _spawnedTransform;

        private readonly Queue<T> _pool = new();

        public ClientPool(T prefab, int initSize,
            Transform poolTransform, Transform spawnedTransform)
        {
            _prefab = prefab;
            _initSize = initSize;

            _poolTransform = poolTransform;
            _spawnedTransform = spawnedTransform;

            var prefabGO = _prefab.gameObject;

            if (prefabGO.TryGetComponent<NetworkIdentity>(out var identity))
            {
                if (!NetworkClient.prefabs.ContainsKey(identity.assetId))
                {
                    NetworkClient.RegisterPrefab(prefabGO, SpawnHandler, UnspawnHandler);
                }
            }
        }

        public void Dispose()
        {
            var prefabGO = _prefab.gameObject;

            NetworkClient.UnregisterPrefab(prefabGO);
        }

        public void ServerPopulatePool()
        {
            for (int i = 0; i < _initSize; i++)
            {
                var item = CreateNewItem();
                NetworkServer.Spawn(item.gameObject);
                NetworkServer.UnSpawn(item.gameObject);
            }
        }

        public T Spawn()
        {
            if (!_pool.TryDequeue(out var item))
            {
                item = CreateNewItem();
            }

            PrepareForSpawn(item);
            item.transform.SetParent(_spawnedTransform);
            item.gameObject.SetActive(true);

            return item;
        }

        public void Unspawn(T item)
        {
            PrepareForUnspawn(item);
            _pool.Enqueue(item);
        }

        private GameObject SpawnHandler(SpawnMessage msg)
        {
            var item = Spawn();

            return item.gameObject;
        }

        private void UnspawnHandler(GameObject spawnedGO)
        {
            if (!spawnedGO.TryGetComponent<T>(out var item))
            {
                return;
            }

            Unspawn(item);
        }


        private T CreateNewItem()
        {
            var itemGO = GameObject.Instantiate(_prefab);
            var item = itemGO.GetComponent<T>();

            PrepareForUnspawn(item);
            return item;
        }

        protected virtual void PrepareForSpawn(T item)
        {

        }

        protected virtual void PrepareForUnspawn(T item)
        {
            if (item.gameObject.activeInHierarchy)
            {
                item.transform.SetParent(_poolTransform, false);
            }

            item.gameObject.SetActive(false);
        }
    }
}