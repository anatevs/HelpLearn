using UnityEngine;
using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class TargetSpawnService :
        ISpawnService
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;

        public List<IInfoPool> InfoPools => _infoPools;

        private readonly IPool<Target> _pool;

        private readonly MovablesSystem _movablesSystem;

        private readonly List<IInfoPool> _infoPools;

        public TargetSpawnService(IPool<Target> pool, MovablesSystem movablesSystem)
        {
            _pool = pool;
            _movablesSystem = movablesSystem;

            if (pool is IInfoPool infoPool)
            {
                _infoPools ??= new();

                _infoPools.Add(infoPool);
            }
        }

        public Target Spawn(Transform spawnPoint, float speed)
        {
            var target = _pool.Get();

            target.transform.position = spawnPoint.position;

            target.SetParameters(speed, spawnPoint.forward);

            target.Activate(true);

            target.OnKilled += Unspawn;

            OnSpawned?.Invoke();

            _movablesSystem.AddMovable(target);

            return target;
        }

        public void Unspawn(Target target)
        {
            target.OnKilled -= Unspawn;

            _movablesSystem.RemoveMovable(target);

            target.SetParameters(0, Vector3.zero);

            _pool?.Release(target);

            OnUnspawned?.Invoke();
        }
    }
}