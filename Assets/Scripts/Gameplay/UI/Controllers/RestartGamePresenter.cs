using GameManagement;
using System;

namespace UI
{
    public class RestartGamePresenter :
        IDisposable
    {
        private readonly IRestartGameView _view;

        private readonly GameResetService _gameResetService;

        private readonly GameStateMachine _gameStateMachine;

        public RestartGamePresenter(IRestartGameView view,
            GameResetService gameResetService,
            GameStateMachine gameStateMachine)
        {
            _view = view;
            _gameResetService = gameResetService;
            _gameStateMachine = gameStateMachine;

            _view.OnRestartClicked += Reset;
        }

        public void Dispose()
        {
            _view.OnRestartClicked -= Reset;
        }

        private void Reset()
        {
            _gameResetService.ResetGame();

            _gameStateMachine.ChangeState(new GameplayState());
        }
    }
}