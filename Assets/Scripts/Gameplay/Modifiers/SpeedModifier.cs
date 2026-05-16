using Input;
using System;

namespace Gameplay
{
    public class SpeedModifier : IGameModifier
    {
        public string Name => _config.Name;

        public Type ConfigType => _config.GetType();

        private readonly IInputSwitchService _inputSwitchService;

        private readonly SpeedModifierConfig _config;

        private readonly IInputService _currentInput;

        public SpeedModifier(SpeedModifierConfig config, IInputSwitchService inputSwitchService)
        {
            _config = config;
            _inputSwitchService = inputSwitchService;
            _currentInput = _inputSwitchService.CurrentInput;
        }

        public void HandleInputSwitch(IInputService newInput)
        {
            if (_currentInput != newInput)
            {
                OnEnterGameplay();
            }
        }

        public void OnEnterGameplay()
        {
            _inputSwitchService.CurrentInput.Speed *= _config.Multiplier;
            _inputSwitchService.OnInputSwitched += HandleInputSwitch;
        }

        public void OnExitGameplay()
        {
            _inputSwitchService.CurrentInput.Speed /= _config.Multiplier;
            _inputSwitchService.OnInputSwitched -= HandleInputSwitch;
        }

        public void Tick(float deltaTime)
        {
        }
    }
}