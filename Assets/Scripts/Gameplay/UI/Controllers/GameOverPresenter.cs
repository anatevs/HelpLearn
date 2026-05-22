using GameManagement;
using System;

namespace UI
{
    public sealed class GameOverPresenter :
        IDisposable
    {
        private readonly IGameOverView _view;

        private readonly MainMenuPresenter _mainMenuPresenter;

        private readonly GameStatesService _gameStateService;

        public GameOverPresenter(IGameOverView view,
            MainMenuPresenter mainMenuPresenter,
            GameStatesService statesService)
        {
            _view = view;
            _mainMenuPresenter = mainMenuPresenter;
            _gameStateService = statesService;

            _view.OnToMainMenu += TransitToMainMenu;
            _gameStateService = statesService;
        }

        public void Dispose()
        {
            _view.OnToMainMenu -= TransitToMainMenu;
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
            _gameStateService.SetMainMenu(_mainMenuPresenter);
        }
    }
}