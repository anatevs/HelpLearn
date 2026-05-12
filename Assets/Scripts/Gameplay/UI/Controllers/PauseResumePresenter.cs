using GameManagement;
using System;

namespace UI
{
    public class PauseResumePresenter : IDisposable
    {
        private readonly IPauseResumeView _view;

        private readonly GameStateMachine _gameStateMachine;

        public PauseResumePresenter(IPauseResumeView view, GameStateMachine gameStateMachine)
        {
            _view = view;
            _gameStateMachine = gameStateMachine;

            _view.OnPaused += HandlePause;
            _view.OnResumed += HandleResume;
        }

        public void Dispose()
        {
            _view.OnPaused -= HandlePause;
            _view.OnResumed -= HandleResume;
        }

        private void HandlePause()
        {
            _gameStateMachine.ChangeState(new PauseState());
        }

        private void HandleResume()
        {
            _gameStateMachine.ChangeState(new GameplayState());
        }
    }
}