using EventBusNamespace;
using System;
using System.Collections.Generic;

namespace GameManagement
{
    public sealed class GameStateService : 
        IDisposable
    {
        private readonly EventBus _eventBus;

        private IGameState _currentState = new InitGameState();

        private Dictionary<GameStateType, Func<IGameState>> _newStates = new();

        public GameStateService(EventBus eventBus)
        {
            _eventBus = eventBus;

            _newStates.Add(GameStateType.Init, () => new InitGameState());
            _newStates.Add(GameStateType.Playing, () => new PlayingGameState());
            _newStates.Add(GameStateType.Paused, () => new PauseGameState());
            _newStates.Add(GameStateType.Win, () => new WinGameState());
            _newStates.Add(GameStateType.Lose, () => new LoseGameState());

            Init();
        }

        private void Init()
        {
            _eventBus.Subscribe<GameInitEvent>(SetState);
            _eventBus.Subscribe<GamePlayingEvent>(SetState);
            _eventBus.Subscribe<GamePausedEvent>(SetState);
            _eventBus.Subscribe<GameWinEvent>(SetState);
            _eventBus.Subscribe<GameLoseEvent>(SetState);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<GameInitEvent>(SetState);
            _eventBus.Unsubscribe<GamePlayingEvent>(SetState);
            _eventBus.Unsubscribe<GamePausedEvent>(SetState);
            _eventBus.Unsubscribe<GameWinEvent>(SetState);
            _eventBus.Unsubscribe<GameLoseEvent>(SetState);
        }

        private void SetState<T>(T e) where T : ChangeStateEvent
        {
            SetState(e.State);
        }

        private void SetState(GameStateType state)
        {
            _currentState.Exit();

            _currentState = _newStates[state].Invoke();

            _currentState.Enter();
        }
    }
}