using EventBusNamespace;
using System;
using System.Collections.Generic;

namespace GameManagement
{
    public sealed class GameStateService : DDOLClass<GameStateService>
    {
        private IGameState _currentState = new InitGameState();

        private Dictionary<GameStateType, Func<IGameState>> _newStates = new();

        private void Awake()
        {
            _newStates.Add(GameStateType.Init, () => new InitGameState());
            _newStates.Add(GameStateType.Playing, () => new PlayingGameState());
            _newStates.Add(GameStateType.Paused, () => new PauseGameState());
            _newStates.Add(GameStateType.Win, () => new WinGameState());
            _newStates.Add(GameStateType.Lose, () => new LoseGameState());
        }

        private void OnEnable()
        {
            EventBus.Subscribe<GameInitEvent>(SetState);
            EventBus.Subscribe<GamePlayingEvent>(SetState);
            EventBus.Subscribe<GamePausedEvent>(SetState);
            EventBus.Subscribe<GameWinEvent>(SetState);
            EventBus.Subscribe<GameLoseEvent>(SetState);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<GameInitEvent>(SetState);
            EventBus.Unsubscribe<GamePlayingEvent>(SetState);
            EventBus.Unsubscribe<GamePausedEvent>(SetState);
            EventBus.Unsubscribe<GameWinEvent>(SetState);
            EventBus.Unsubscribe<GameLoseEvent>(SetState);
        }

        private void SetState<T>(T e) where T : ChangeStateEvent
        {
            SetState(e.State);
        }

        private void SetState(GameStateType state)
        {
            _currentState = _newStates[state].Invoke();

            _currentState.Enter();
        }
    }
}