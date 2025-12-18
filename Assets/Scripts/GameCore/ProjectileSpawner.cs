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

        private Queue<Projectile> _pool = new();

        private List<Projectile> _activeProjectiles = new();

        private void Awake()
        {
            for (int i = 0; i < _initCount; i++)
            {
                var item = Instantiate(_prefab, _poolTransform);
                item.gameObject.SetActive(false);

                _pool.Enqueue(item);
            }
        }

        private void OnDisable()
        {
            foreach (var item in _activeProjectiles)
            {
                item.Destroyed -= Unspawn;
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

            item.Destroyed += Unspawn;
            _activeProjectiles.Add(item);

            return item;
        }

        public void Unspawn(Projectile item)
        {
            item.Destroyed -= Unspawn;
            _activeProjectiles.Remove(item);

            item.gameObject.SetActive(false);
            item.transform.SetParent(_poolTransform);
            _pool.Enqueue(item);
        }
    }
}