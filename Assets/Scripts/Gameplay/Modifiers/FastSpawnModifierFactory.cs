using System;

namespace Gameplay
{
    public sealed class FastSpawnModifierFactory : IModifierFactory
    {
        public Type ConfigType => typeof(FastSpawnModifierConfig);

        private readonly IItemsSpawner _spawner;

        public FastSpawnModifierFactory(IItemsSpawner spawner)
        {
            _spawner = spawner;
        }

        public IGameModifier Create(GameModifierConfig config)
        {
            return new FastSpawnModifier((FastSpawnModifierConfig)config, _spawner);
        }
    }
}