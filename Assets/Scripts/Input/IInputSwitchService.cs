using GameManagement;
using System;

namespace Input
{
    public interface IInputSwitchService : IResetable,
        IDisposable
    {
        public event Action<IInputService> OnInputSwitched;

        public IInputService CurrentInput { get; }

        public void SwitchInput(IInputService externalInput);

        public void SwitchInput();
    }
}