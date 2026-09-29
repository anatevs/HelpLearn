using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class PlayingGameState : IGameState
    {
        private readonly PausablesManager _pausables;

        public PlayingGameState(PausablesManager pausables)
        {
            _pausables = pausables;
        }

        public void Enter()
        {
            _pausables.Resume();

            Time.timeScale = 1f;
        }

        public void Exit()
        {
            
        }
    }
}