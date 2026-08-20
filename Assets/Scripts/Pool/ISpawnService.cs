using System;
using System.Collections.Generic;

namespace Gameplay
{
    public interface ISpawnService
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;
        public string SpawnObjectName { get; }

        public List<IInfoPool> InfoPools {get;}
    }
}