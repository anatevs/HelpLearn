using System;

namespace UI
{
    public interface ISwitchInputView
    {
        public event Action OnInputSwitched;

        public void SetType(string type);
    }
}