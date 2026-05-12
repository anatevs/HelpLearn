using UI;
using UnityEngine;

namespace GameManagement
{
    public class MainMenuState : IGameState
    {
        private readonly MainMenuPresenter _mainMenuPresenter;

        public MainMenuState(MainMenuPresenter mainMenuPresenter)
        {
            _mainMenuPresenter = mainMenuPresenter;
        }

        public void Enter()
        {
            Time.timeScale = 0f;

            _mainMenuPresenter.Show();
        }

        public void Exit()
        {
            Time.timeScale = 1f;

            _mainMenuPresenter.Hide();
        }
    }
}