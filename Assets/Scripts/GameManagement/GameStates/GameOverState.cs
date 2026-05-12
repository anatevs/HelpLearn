using UI;
using UnityEngine;

namespace GameManagement
{
    public sealed class GameOverState : IGameState
    {
        private readonly bool _isWin;
        private readonly GameOverPresenter _presenter;

        public GameOverState(bool isWin,
            GameOverPresenter presenter)
        {
            _isWin = isWin;
            _presenter = presenter;
        }

        public void Enter()
        {
            Time.timeScale = 0f;

            _presenter.Show(_isWin);
        }

        public void Exit()
        {
            Time.timeScale = 1f;

            _presenter.Hide();
        }
    }
}