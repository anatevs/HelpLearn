using EventBusNamespace;
using System;
using UnityEngine;

namespace GameManagement
{
    public sealed class GameStateService : 
        IDisposable
    {
        private readonly EventBus _eventBus;

        public GameState CurrentState => _currentState;

        private GameState _currentState = GameState.Init;

        public GameStateService(EventBus eventBus)
        {
            _eventBus = eventBus;
            Init();
        }

        private void Init()
        {
            _eventBus.Subscribe<ChangeGameStateEvent>(ChangeState);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<ChangeGameStateEvent>(ChangeState);
        }

        private void ChangeState(ChangeGameStateEvent e)
        {
            SetState(e.Value);
        }

        private void SetState(GameState state)
        {
            _currentState = state;

            if (_currentState == GameState.Paused)
            {
                Time.timeScale = 0;
            }
            else if (_currentState == GameState.Playing)
            {
                Time.timeScale = 1;
            }
            else if (_currentState == GameState.Win || _currentState == GameState.Lose)
            {
                Time.timeScale = 0;
            }
        }
    }
}