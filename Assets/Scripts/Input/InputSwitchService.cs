using System;

namespace Input
{
    public class InputSwitchService : IInputSwitchService
    {
        public event Action<IInputService> OnInputSwitched;

        public IInputService CurrentInput => _input;

        private readonly IInputService[] _inputServices;

        private IInputService _input;

        private readonly int _initInputIndex = 0;
        private int _currentIndex = 0;

        public InputSwitchService(IInputService[] inputServices,
            int initInputIndex)
        {
            _inputServices = inputServices;
            _initInputIndex = initInputIndex;
            _currentIndex = _initInputIndex;

            _input = _inputServices[_currentIndex];
        }

        public void Dispose()
        {
            _input.Dispose();
        }

        public void ResetLevel()
        {
            SwitchInput(_initInputIndex);
        }

        public void SwitchInput(IInputService externalInput)
        {
            _currentIndex = -1;

            SetInput(externalInput);
        }

        public void SwitchInput()
        {
            var newIndex = (_currentIndex + 1) % _inputServices.Length;

            SwitchInput(newIndex);
        }

        private void SwitchInput(int index)
        {
            _currentIndex = index;

            SetInput(_inputServices[_currentIndex]);
        }

        private void SetInput(IInputService input)
        {
            _input?.Disable();
            _input = input;

            _input.ResetLevel();

            OnInputSwitched?.Invoke(_input);
        }
    }
}