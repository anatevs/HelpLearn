using Input;
using System;

namespace UI
{
    public class InputSwitchPresenter :
        IDisposable
    {
        public readonly ISwitchInputView _switchInputView;

        private readonly IInputSwitchService _inputSwitchService;

        public InputSwitchPresenter(IInputSwitchService inputSwitchService,
            ISwitchInputView switchInputView)
        {
            _inputSwitchService = inputSwitchService;
            _switchInputView = switchInputView;

            _switchInputView.OnInputSwitched += _inputSwitchService.SwitchInput;
            _inputSwitchService.OnInputSwitched += HandleSwitch;

            _switchInputView.SetType(_inputSwitchService.CurrentInput.TypeName);
        }

        public void Dispose()
        {
            _switchInputView.OnInputSwitched -= _inputSwitchService.SwitchInput;
            _inputSwitchService.OnInputSwitched -= HandleSwitch;
        }

        private void HandleSwitch(IInputService input)
        {
            _switchInputView.SetType(input.TypeName);
        }
    }
}