using System;
using UI;

namespace Input
{
    public class InputSwitchBinder :
        IDisposable
    {
        private readonly IInputSwitchService _inputSwitchService;

        private readonly PlayerController _player;

        private readonly ILoggerService _loggerService;

        public InputSwitchBinder(IInputSwitchService inputSwitchService,
            PlayerController player,
            ILoggerService loggerService)
        {
            _inputSwitchService = inputSwitchService;
            _player = player;
            _loggerService = loggerService;

            _inputSwitchService.OnInputSwitched += HandleSwitch;
        }

        public void Dispose()
        {
            _inputSwitchService.OnInputSwitched -= HandleSwitch;
        }

        private void HandleSwitch(IInputService input)
        {
            _player.SetInput(input);

            _loggerService.LogInputSwitched(input.TypeName);
        }
    }
}