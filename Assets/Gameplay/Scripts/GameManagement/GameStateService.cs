using EventBusNamespace;
using UnityEngine;

namespace GameManagement
{
    public sealed class GameStateService : DDOLClass<GameStateService>
    {
        public GameState CurrentState => _currentState;

        private GameState _currentState = GameState.Init;

        private void OnEnable()
        {
            EventBus.Subscribe<ChangeGameStateEvent>(ChangeState);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<ChangeGameStateEvent>(ChangeState);
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