using System;
using System.Collections.Generic;

namespace Gameplay
{
    public sealed class ModifiersCreator
    {
        private readonly Dictionary<Type, IModifierFactory> _factories = new();

        public IModifierFactory GetFactory(Type configType)
        {
            return _factories[configType];
        }

        public IGameModifier[] CreateModifiers(GameModifierConfig[] configs)
        {
            IGameModifier[] modifiers = new IGameModifier[configs.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                modifiers[i] = Create(configs[i]);
            }

            return modifiers;
        }

        public IGameModifier Create(GameModifierConfig config)
        {
            var factory = _factories[config.GetType()];

            return factory.Create(config);
        }

        public void AddFactory(List<IModifierFactory> factories)
        {
            for (int i = 0; i < factories.Count; i++)
            {
                AddFactory(factories[i]);
            }
        }

        public void AddFactory(IModifierFactory factory)
        {
            _factories.Add(factory.ConfigType, factory);
        }
    }
}