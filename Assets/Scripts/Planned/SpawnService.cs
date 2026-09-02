using UnityEngine;
using System;
using System.Collections.Generic;

namespace Gameplay
{
    public abstract class SpawnService<T> : MonoBehaviour,
        ISpawnService
        where T : MonoBehaviour
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;

        protected string _spawnObjectName;

        public Transform PoolTransform => _poolTransform;

        public List<IInfoPool> InfoPools => throw new NotImplementedException();

        [SerializeField]
        private Transform _poolTransform;

        private IPool<T> _pool;

        public void Init(IPool<T> pool)
        {
            _pool = pool;
        }

        public virtual T Spawn(Transform spawnPoint, float speed)
        {
            var target = _pool.Get();

            target.transform.SetParent(transform);

            target.transform.position = spawnPoint.position;

            target.gameObject.SetActive(true);

            OnSpawned?.Invoke();

            return target;
        }

        public virtual void Unspawn(T target)
        {
            target.gameObject.SetActive(false);

            target.transform.SetParent(_poolTransform, false);

            _pool?.Release(target);

            OnUnspawned?.Invoke();
        }
    }
}