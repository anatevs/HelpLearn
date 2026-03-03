using UnityEngine;

namespace GameManagement
{
    public class PauseGameState : IGameState
    {
        public void Enter()
        {
            Time.timeScale = 0f;
        }
    }
}