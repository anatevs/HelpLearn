using GameManagement;
using System;

namespace UI
{
    public class RestartGamePresenter :
        IDisposable
    {
        private readonly IRestartGameView _view;

        private readonly GameResetService _gameResetService;

        //private readonly GameStateMachine _gameStateMachine;
        private readonly GameStatesService _gameStatesService;

        public RestartGamePresenter(IRestartGameView view,
            GameResetService gameResetService,
            GameStatesService statesService)
            //GameStateMachine gameStateMachine)
        {
            _view = view;
            _gameResetService = gameResetService;
            _gameStatesService = statesService;
            //_gameStateMachine = gameStateMachine;

            _view.OnRestartClicked += Reset;
        }

        public void Dispose()
        {
            _view.OnRestartClicked -= Reset;
        }

        private void Reset()
        {
            _gameResetService.ResetGame();

            _gameStatesService.SetGameplay();
            //_gameStateMachine.ChangeState(new GameplayState());
        }
    }
}