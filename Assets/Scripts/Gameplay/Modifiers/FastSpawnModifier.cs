using System;

namespace Gameplay
{
    public sealed class FastSpawnModifier : IGameModifier
    {
        public string Name => _config.Name;

        public Type ConfigType => _config.GetType();

        private readonly FastSpawnModifierConfig _config;

        private readonly IItemsSpawner _spawner;

        public FastSpawnModifier(FastSpawnModifierConfig config,
            IItemsSpawner spawner)
        {
            _config = config;
            _spawner = spawner;
        }

        public void OnEnterGameplay()
        {
            _spawner.MultiplySpawnPeriod(1 / _config.PeriodDivider);
        }

        public void OnExitGameplay()
        {
            _spawner.MultiplySpawnPeriod(_config.PeriodDivider);
        }

        public void Tick(float deltaTime)
        {
        }
    }
}