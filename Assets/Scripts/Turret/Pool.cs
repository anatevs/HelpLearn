using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Pool<T> : IPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;

        private readonly int _initCount;

        private readonly Queue<T> _pool = new();

        public Pool(T prefab, int initCount)
        {
            _prefab = prefab;
            _initCount = initCount;

            PopulatePool();
        }

        public T Get()
        {
            if (!_pool.TryDequeue(out var item))
            {
                item = CreateNew();
            }

            return item;
        }

        public void Release(T item)
        {
            item.gameObject.SetActive(false);

            _pool.Enqueue(item);
        }

        private void PopulatePool()
        {
            for (int i = 0; i < _initCount; i++)
            {
                var item = CreateNew();

                _pool.Enqueue(item);
            }
        }

        private T CreateNew()
        {
            var item = GameObject.Instantiate(_prefab);
            item.gameObject.SetActive(false);

            return item;
        }
    }
}