using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Pool<T> where T : Component
    {
        private readonly T _prefab;

        private readonly int _initNumber;

        private readonly Transform _poolParent;

        private readonly Queue<T> _pool = new();

        public Pool(T prefab, int initNumber, Transform poolParent)
        {
            _prefab = prefab;
            _initNumber = initNumber;
            _poolParent = poolParent;

            for (int i = 0; i < _initNumber; i++)
            {
                _pool.Enqueue(InitNewItem());
            }
        }

        public T Spawn(Transform spawnPoint)
        {
            if (!_pool.TryDequeue(out var item))
            {
                item = InitNewItem();
            }

            _pool.Enqueue(item);

            item.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

            item.gameObject.SetActive(true);

            return item;
        }

        public void Unspawn(T item)
        {
            item.gameObject.SetActive(false);
            item.transform.parent = _poolParent;

            _pool.Enqueue(item);
        }

        private T InitNewItem()
        {
            T item = GameObject.Instantiate(_prefab);
            item.gameObject.SetActive(false);
            item.transform.parent = _poolParent;

            return item;
        }
    }
}