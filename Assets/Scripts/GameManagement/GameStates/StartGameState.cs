using UnityEngine;

namespace GameManagement
{
    public class StartGameState : IGameState
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