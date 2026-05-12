using System;
using GameManagement;

namespace UI
{
    public class MainMenuPresenter :
        IDisposable
    {
        private readonly IMainMenuView _mainMenuView;

        private readonly RestartGamePresenter _restartGamePresenter;

        private readonly IGameExit _gameExit;

        public MainMenuPresenter(IMainMenuView mainMenuView,
            GameResetService gameResetService,
            GameStateMachine gameStateMachine,
            IGameExit gameExit)
        {
            _mainMenuView = mainMenuView;
            _restartGamePresenter = new RestartGamePresenter(_mainMenuView.RestartView, gameResetService, gameStateMachine);
            _gameExit = gameExit;

            _mainMenuView.OnExitClicked += Exit;
        }

        public void Dispose()
        {
            _restartGamePresenter.Dispose();
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