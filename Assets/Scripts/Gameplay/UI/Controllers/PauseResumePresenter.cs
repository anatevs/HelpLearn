using GameManagement;
using System;

namespace UI
{
    public class PauseResumePresenter : IDisposable
    {
        private readonly IPauseResumeView _view;

        private readonly GameStatesService _gameStatesService;

        public PauseResumePresenter(IPauseResumeView view,
            GameStatesService gameStatesService)
        {
            _view = view;
            _gameStatesService = gameStatesService;

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
            _gameStatesService.SetPause();
        }

        private void HandleResume()
        {
            _gameStatesService.SetGameplay();
        }
    }
}