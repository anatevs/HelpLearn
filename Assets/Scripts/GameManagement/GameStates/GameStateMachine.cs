namespace GameManagement
{
    public class GameStateMachine
    {
        private IGameState _currentState;

        public GameStateMachine(IGameState initState)
        {
            _currentState = initState;

            _currentState.Enter();
        }

        public void ChangeState(IGameState newState)
        {
            _currentState.Exit();

            _currentState = newState;

            _currentState.Enter();
        }
    }
}