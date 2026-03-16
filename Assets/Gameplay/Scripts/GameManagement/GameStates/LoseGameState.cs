using UnityEngine;

namespace GameManagement
{
    public class LoseGameState : IGameState
    {
        public void Enter()
        {
            Time.timeScale = 0f;
        }
    }
}