using System;

namespace UI
{
    public interface IPauseView
    {
        public event Action OnResumeClicked;
        public event Action OnToMenuClicked;
    }
}