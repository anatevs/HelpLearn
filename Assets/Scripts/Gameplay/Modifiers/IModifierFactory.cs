using System;

namespace Gameplay
{
    public interface IModifierFactory
    {
        public Type ConfigType { get; }
        public IGameModifier Create(GameModifierConfig config);
    }
}