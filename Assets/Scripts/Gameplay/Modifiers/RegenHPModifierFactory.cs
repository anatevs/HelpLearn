using System;

namespace Gameplay
{
    public sealed class RegenHPModifierFactory : IModifierFactory
    {
        public Type ConfigType => typeof(RegenHPModifierConfig);

        private readonly IHealth _health;

        public RegenHPModifierFactory(IHealth health)
        {
            _health = health;
        }

        public IGameModifier Create(GameModifierConfig config)
        {
            return new RegenHPModifier((RegenHPModifierConfig)config, _health);
        }
    }
}