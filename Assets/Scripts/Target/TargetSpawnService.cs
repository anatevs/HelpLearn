using UnityEngine;
using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class TargetSpawnService : MonoBehaviour,
        ISpawnService
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;
        public string SpawnObjectName => "Targets";

        public Transform PoolTransform => _poolTransform;

        public List<IInfoPool> InfoPools => _infoPools;

        [SerializeField]
        private Transform _poolTransform;

        private IPool<Target> _pool;

        private List<IInfoPool> _infoPools;

        public void InitPool(IPool<Target> pool)
        {
            _pool = pool;

            if (pool is IInfoPool infoPool)
            {
                _infoPools ??= new();

                _infoPools.Add(infoPool);
            }
        }

        public Target Spawn(Transform spawnPoint, float speed)
        {
            var target = _pool.Get();

            target.transform.SetParent(transform);

            target.transform.position = spawnPoint.position;

            target.SetParameters(speed, spawnPoint.forward);

            target.gameObject.SetActive(true);

            target.OnKilled += Unspawn;

            OnSpawned?.Invoke();

            return target;
        }

        public void Unspawn(Target target)
        {
            target.OnKilled -= Unspawn;

            target.gameObject.SetActive(false);

            target.SetParameters(0, Vector3.zero);

            target.transform.SetParent(_poolTransform, false);

            _pool?.Release(target);

            OnUnspawned?.Invoke();
        }
    }
}