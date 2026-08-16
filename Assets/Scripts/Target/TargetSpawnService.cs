using UnityEngine;
using System;

namespace Gameplay
{
    public class TargetSpawnService : MonoBehaviour,
        ISpawnService
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;
        public string SpawnObjectName => "Targets";

        [SerializeField]
        private Transform _poolTransform;

        private IPool<Target> _pool;

        public void Init(IPool<Target> pool)
        {
            _pool = pool;
        }

        public Target Spawn(Transform spawnPoint, float speed)
        {
            var target = _pool.Get();

            target.transform.SetParent(transform);

            target.transform.position = spawnPoint.position;

            target.Init(speed, spawnPoint.forward);

            target.gameObject.SetActive(true);

            target.OnKilled += Unspawn;

            OnSpawned?.Invoke();

            return target;
        }

        public void Unspawn(Target target)
        {
            target.OnKilled -= Unspawn;

            target.gameObject.SetActive(false);

            target.transform.SetParent(_poolTransform, false);

            _pool?.Release(target);

            OnUnspawned?.Invoke();
        }
    }
}