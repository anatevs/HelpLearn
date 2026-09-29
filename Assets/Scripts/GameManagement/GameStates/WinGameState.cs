using Gameplay;
using UI;
using UnityEngine;

namespace GameManagement
{
    public sealed class WinGameState : IGameState
    {
        private readonly EndGameController _endGameController;
        private readonly PausablesManager _pausables;

        public WinGameState(EndGameController endGameController, PausablesManager pausables)
        {
            _endGameController = endGameController;
            _pausables = pausables;
        }

        public void Enter()
        {
            _pausables.Pause();
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