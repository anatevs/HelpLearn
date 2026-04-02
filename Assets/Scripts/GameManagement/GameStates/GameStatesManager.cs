using System;
using System.Collections.Generic;

namespace GameManagement
{
    public sealed class GameStatesManager
    {
        private IGameState _currentState = new StartGameState();

        private readonly Dictionary<GameStateType, Func<IGameState>> _newStates = new();

        //public GameStatesManager()
        //{
        //    _newStates.Add(GameStateType.Start, () => new StartGameState());
        //    _newStates.Add(GameStateType.Playing, () => new PlayingGameState());
        //    _newStates.Add(GameStateType.Win, () => new WinGameState());
        //    _newStates.Add(GameStateType.Lose, () => new LoseGameState());
        //}

        //public void SetState(GameStateType state)
        //{
        //    _currentState.Exit();

        //    _currentState = _newStates[state].Invoke();

        //    _currentState.Enter();
        //}

        public void SetState(IGameState state)
        {
            _currentState.Exit();

            _currentState = state;

            _currentState.Enter();
        }
    }
}