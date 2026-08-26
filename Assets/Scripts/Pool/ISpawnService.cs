using System;
using System.Collections.Generic;

namespace Gameplay
{
    public interface ISpawnService
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;

        public List<IInfoPool> InfoPools {get;}
    }
}