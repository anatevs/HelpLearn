using UnityEngine;
using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class TargetSpawnService :
        ISpawnService,
        IDisposable
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;

        public List<IInfoPool> InfoPools => _infoPools;

        private readonly IPool<Target> _pool;

        private readonly MovablesSystem _movablesSystem;

        private readonly List<IInfoPool> _infoPools;

        private readonly TargetsService _targetService;

        public TargetSpawnService(IPool<Target> pool, MovablesSystem movablesSystem)
        {
            _pool = pool;
            _movablesSystem = movablesSystem;

            if (pool is IInfoPool infoPool)
            {
                _infoPools ??= new();

                _infoPools.Add(infoPool);
            }

            _targetService = new TargetsService(this);
            _targetService.AddPool(pool);
        }

        public void Dispose()
        {
            _targetService.Dispose();
        }

        public Target Spawn(Transform spawnPoint, float speed)
        {
            var target = _pool.Get();

            target.transform.position = spawnPoint.position;

            target.SetParameters(speed, spawnPoint.forward);

            target.Activate(true);

            OnSpawned?.Invoke();

            _movablesSystem.AddMovable(target);

            return target;
        }

        public void Unspawn(Target target)
        {
            _movablesSystem.RemoveMovable(target);

            target.SetParameters(0, Vector3.zero);

            _pool.Release(target);

            OnUnspawned?.Invoke();
        }
    }
}