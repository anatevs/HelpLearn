using System;
using GameManagement;

namespace UI
{
    public class MainMenuPresenter :
        IDisposable
    {
        private readonly IMainMenuView _mainMenuView;

        private readonly IGameExit _gameExit;

        public MainMenuPresenter(IMainMenuView mainMenuView,
            IGameExit gameExit)
        {
            _mainMenuView = mainMenuView;

            _gameExit = gameExit;

            _mainMenuView.OnExitClicked += Exit;
        }

        public void Dispose()
        {
            _mainMenuView.OnExitClicked -= Exit;
        }

        public void Show()
        {
            _mainMenuView.Show();
        }

        public void Hide()
        {
            _mainMenuView.Hide();
        }

        private void Exit()
        {
            _gameExit.QuitGame();
        }
    }
}