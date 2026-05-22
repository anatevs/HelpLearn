using UI;

namespace GameManagement
{
    public class GameStatesService
    {
        private readonly GameStateMachine _gameStateMachine;

        private readonly InitState _initState = new();
        private readonly GameplayState _gameplayState = new();
        private readonly PauseState _pauseState = new();

        private MainMenuState _mainMenuState;
        private GameOverState _winState;
        private GameOverState _lostState;

        public GameStatesService(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void SetInit()
        {
            _gameStateMachine.ChangeState(_initState);
        }

        public void SetGameplay()
        {
            _gameStateMachine.ChangeState(_gameplayState);
        }

        public void SetPause()
        {
            _gameStateMachine.ChangeState(_pauseState);
        }

        public void SetMainMenu(MainMenuPresenter mainMenuPresenter)
        {
            _mainMenuState ??= new MainMenuState(mainMenuPresenter);

            _gameStateMachine.ChangeState(_mainMenuState);
        }

        public void SetWin(GameOverPresenter gameOverPresenter)
        {
            _winState ??= new GameOverState(true, gameOverPresenter);

            _gameStateMachine.ChangeState(_winState);
        }

        public void SetLost(GameOverPresenter gameOverPresenter)
        {
            _lostState ??= new GameOverState(false, gameOverPresenter);

            _gameStateMachine.ChangeState(_lostState);
        }
    }
}