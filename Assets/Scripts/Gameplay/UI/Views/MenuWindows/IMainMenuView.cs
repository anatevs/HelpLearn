using System;

namespace UI
{
    public interface IMainMenuView
    {
        public event Action OnExitClicked;

        public IRestartGameView RestartView { get; }

        public void Show();
        public void Hide();
    }
}