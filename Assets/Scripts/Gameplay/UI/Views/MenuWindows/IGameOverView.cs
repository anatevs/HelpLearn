using System;

namespace UI
{
    public interface IGameOverView
    {
        public event Action OnToMainMenu;

        public IRestartGameView RestartView { get; }

        public void Show(bool isWin);

        public void Hide();
    }
}