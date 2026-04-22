using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class Pool<T> where T : Component
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

        public T Spawn(Transform parent)
        {
            if (!_pool.TryDequeue(out var item))
            {
                item = InitNewItem();
            }

            item.transform.SetParent(_poolParent, false);

            return item;
        }

        public void Unspawn(T item)
        {
            item.gameObject.SetActive(false);

            item.transform.SetParent(_poolParent, false);

            _pool.Enqueue(item);
        }

        private T InitNewItem()
        {
            T item = GameObject.Instantiate(_prefab);
            item.gameObject.SetActive(false);
            item.transform.SetParent(_poolParent, false);
            item.transform.localPosition = Vector3.zero;

            return item;
        }
    }
}