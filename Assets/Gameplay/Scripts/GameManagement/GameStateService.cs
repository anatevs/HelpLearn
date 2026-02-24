using System;
using UnityEngine;

namespace GameManagement
{
    public class GameStateService : DDOLClass<GameStateService>
    {
        public event Action<GameState> OnGameStateChanged;

        public GameState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnGameStateChanged?.Invoke(value);
            }
        }

        private GameState _currentState = GameState.Init;


    }
}