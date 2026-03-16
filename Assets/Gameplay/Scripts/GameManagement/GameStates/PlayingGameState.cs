using UI;
using UnityEngine;

namespace GameManagement
{
    public class PlayingGameState : IGameState
    {
        public void Enter()
        {
            Time.timeScale = 1.0f;
        }

        public void Exit()
        {

        }
    }
}