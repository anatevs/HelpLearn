using System;

namespace Gameplay
{
    public interface ISpawnService
    {
        public event Action OnSpawned;
        public event Action OnUnspawned;
        public string SpawnObjectName { get; }
    }
}