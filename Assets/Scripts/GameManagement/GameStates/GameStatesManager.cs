using System;
using System.Collections.Generic;

namespace GameManagement
{
    public sealed class GameStatesManager
    {
        private IGameState _currentState = new StartGameState();

        public void SetState(IGameState state)
        {
            _currentState.Exit();

            _currentState = state;

            _currentState.Enter();
        }
    }
}