using UnityEngine;

namespace GameManagement
{
    public class PlayingGameState : IGameState
    {
        public void Enter()
        {
            Time.timeScale = 1f;
        }

        public void Exit()
        {
            
        }
    }
}