using Input;
using System;

namespace Gameplay
{
    public sealed class SpeedModifierFactory :
        IModifierFactory
    {
        public Type ConfigType => typeof(SpeedModifierConfig);

        private readonly IInputSwitchService _inputSwitchService;

        public SpeedModifierFactory(IInputSwitchService inputSwitchService)
        {
            _inputSwitchService = inputSwitchService;
        }

        public IGameModifier Create(GameModifierConfig config)
        {
            return new SpeedModifier((SpeedModifierConfig)config, _inputSwitchService);
        }
    }
}