using Input;

namespace Gameplay
{
    public class SpeedModifier : IGameModifier
    {
        public string Name => _config.Name;

        private readonly IInputSwitchService _inputSwitchService;

        private SpeedModifierConfig _config;

        public SpeedModifier(IInputSwitchService inputSwitchService, SpeedModifierConfig config)
        {
            _inputSwitchService = inputSwitchService;
            _config = config;

            _inputSwitchService.OnInputSwitched += HandleInputSwitch;
        }

        public void Dispose()
        {
            _inputSwitchService.OnInputSwitched -= HandleInputSwitch;
        }

        public void HandleInputSwitch(IInputService _)
        {
            OnEnterGameplay();
        }

        public void OnEnterGameplay()
        {
            _inputSwitchService.CurrentInput.Speed *= _config.Multiplier;
        }

        public void OnExitGameplay()
        {
            _inputSwitchService.CurrentInput.Speed /= _config.Multiplier;
        }

        public void Tick(float deltaTime)
        {
        }
    }
}