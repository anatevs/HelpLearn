using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class ModifiersSpawner
    {
        private readonly ModifiersCreator _modifiersCreator;

        private readonly Dictionary<Type, Queue<IGameModifier>> _pools = new();

        public ModifiersSpawner(ModifiersCreator modifiersCreator)
        {
            _modifiersCreator = modifiersCreator;
        }

        public IGameModifier Create(GameModifierConfig config)
        {
            if (!(_pools.TryGetValue(config.GetType(), out var modifiers) &&
                modifiers.TryDequeue(out var modifier)))
            {
                modifier = _modifiersCreator.Create(config);
            }

            return modifier;
        }

        public void ReleaseModifier(IGameModifier modifier)
        {
            if (!_pools.TryGetValue(modifier.ConfigType, out var pool))
            {
                pool = new Queue<IGameModifier>();
                _pools.Add(modifier.ConfigType, pool);
            }

            pool.Enqueue(modifier);
        }
    }
}