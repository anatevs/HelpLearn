using UI;
using UnityEngine;

namespace GameManagement
{
    public sealed class WinGameState : IGameState
    {
        private readonly EndGameController _endGameController;

        public WinGameState(EndGameController endGameController)
        {
            _endGameController = endGameController;
        }

        public void Enter()
        {
            _endGameController.ShowWin();

            Time.timeScale = 0f;
        }

        public void Exit()
        {
            _endGameController.Hide();

            Time.timeScale = 1f;
        }
    }
}