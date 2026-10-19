using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PoolWithFactory<T> where T : Component, INamed
    {
        public string Name => _name;

        private readonly IObjectsFactory<T> _factory;

        private readonly int _initNumber;

        private readonly Transform _poolParent;

        private readonly Queue<T> _pool = new();

        private readonly string _name;

        public PoolWithFactory(IObjectsFactory<T> factory, int initNumber, Transform poolParent, string name)
        {
            _factory = factory;
            _initNumber = initNumber;
            _poolParent = poolParent;
            _name = name;

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

            _pool.Enqueue(item);

            item.transform.parent = parent;

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
            T item = _factory.Create(_name);

            item.gameObject.SetActive(false);
            item.transform.parent = _poolParent;

            return item;
        }
    }
}