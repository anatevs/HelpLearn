using System;

namespace UI
{
    public interface ISwitchInputController
    {
        public event Action OnInputSwitched;
    }
}