using System;

namespace UI
{
    public interface IGameOverView
    {
        public event Action OnRestartClicked;
        public event Action OnToMenuClicked;
    }
}