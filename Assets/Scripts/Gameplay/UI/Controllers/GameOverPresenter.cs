using GameManagement;
using System;

namespace UI
{
    public sealed class GameOverPresenter :
        IDisposable
    {
        private readonly IGameOverView _view;

        private readonly RestartGamePresenter _restartGamePresenter;

        private readonly MainMenuPresenter _mainMenuPresenter;

        private readonly GameStateMachine _stateMachine;

        public GameOverPresenter(IGameOverView view,
            MainMenuPresenter mainMenuPresenter,
            GameResetService gameResetService,
            GameStateMachine stateMachine)
        {
            _view = view;
            _mainMenuPresenter = mainMenuPresenter;
            _restartGamePresenter = new RestartGamePresenter(_view.RestartView, gameResetService, stateMachine);
            _stateMachine = stateMachine;

            _view.OnToMainMenu += TransitToMainMenu;
        }

        public void Dispose()
        {
            _view.OnToMainMenu -= TransitToMainMenu;
            _restartGamePresenter.Dispose();
        }

        public void Show(bool isWin)
        {
            _view.Show(isWin);
        }

        public void Hide()
        {
            _view.Hide();
        }

        private void TransitToMainMenu()
        {
            _stateMachine.ChangeState(new MainMenuState(_mainMenuPresenter));
        }
    }
}