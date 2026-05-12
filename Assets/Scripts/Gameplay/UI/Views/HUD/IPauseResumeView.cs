using System;

namespace UI
{
    public interface IPauseResumeView
    {
        public event Action OnPaused;
        public event Action OnResumed;

        public void ShowPaused();

        public void ShowResumed();
    }
}