using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public sealed class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField]
        private Projectile _prefab;

        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private int _initCount = 10;

        private readonly Queue<Projectile> _pool = new();

        private void Awake()
        {
            for (int i = 0; i < _initCount; i++)
            {
                var item = Instantiate(_prefab, _poolTransform);
                item.gameObject.SetActive(false);

                _pool.Enqueue(item);
            }
        }

        public Projectile Spawn(Vector3 position, Quaternion rotation, float speed)
        {
            if (!_pool.TryDequeue(out var item))
            {
                item = Instantiate(_prefab);
                item.gameObject.SetActive(false);
            }

            item.Speed = speed;
            item.transform.SetParent(transform);
            item.transform.position = position;
            item.transform.rotation = rotation;
            item.gameObject.SetActive(true);

            return item;
        }

        public void Unspawn(Projectile item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(_poolTransform);
            _pool.Enqueue(item);
        }
    }
}