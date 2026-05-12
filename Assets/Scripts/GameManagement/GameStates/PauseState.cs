using UnityEngine;

namespace GameManagement
{
    public sealed class PauseState : IGameState
    {
        public void Enter()
        {
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
        }
    }
}