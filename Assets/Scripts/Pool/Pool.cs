using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Pool<T> : IPool<T>, IInfoPool, IPrewarmPool
        where T : MonoBehaviour
    {
        public event Action<int> OnPoolSizeChanged;
        public event Action<int> OnCurrentFreeChanged;
        public event Action<int> OnRepeatUsingChanged;

        private readonly T _prefab;

        private readonly Queue<T> _pool = new();

        private readonly Transform _poolTransform;

        private int _poolSize = 0;
        private int _repeatUsing = 0;

        private int _initCount = 0;

        public Pool(T prefab, int initCount, Transform poolTransform)
        {
            _prefab = prefab;
            _poolTransform = poolTransform;

            _initCount = initCount;
        }

        public T Get()
        {
            if (!_pool.TryDequeue(out var item))
            {
                item = CreateNew();
            }
            else
            {
                _repeatUsing++;
                OnRepeatUsingChanged?.Invoke(1);
                OnCurrentFreeChanged?.Invoke(-1);
            }

            return item;
        }

        public void Release(T item)
        {
            item.gameObject.SetActive(false);

            item.transform.SetParent(_poolTransform, false);

            _pool.Enqueue(item);
            OnCurrentFreeChanged?.Invoke(1);
        }

        public void PopulatePool()
        {
            for (int i = 0; i < _initCount; i++)
            {
                var item = CreateNew();

                Release(item);
            }
        }
        public (int Size, int Free, int Repeat) GetInfo()
        {
            return (_poolSize, _pool.Count, _repeatUsing);
        }

        private T CreateNew()
        {
            var item = GameObject.Instantiate(_prefab);
            item.gameObject.SetActive(false);

            _poolSize++;
            OnPoolSizeChanged?.Invoke(1);

            return item;
        }
    }
}