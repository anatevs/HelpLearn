using UI;
using UnityEngine;

namespace GameManagement
{
    public sealed class LoseGameState : IGameState
    {
        private readonly EndGameController _endGameController;

        public LoseGameState(EndGameController endGameController)
        {
            _endGameController = endGameController;
        }

        public void Enter()
        {
            _endGameController.ShowLose();

            Time.timeScale = 0f;
        }

        public void Exit()
        {
            _endGameController.Hide();

            Time.timeScale = 1f;
        }
    }
}