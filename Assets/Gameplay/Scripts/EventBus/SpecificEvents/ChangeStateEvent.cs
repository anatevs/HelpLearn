using GameManagement;

namespace EventBusNamespace
{
    public class ChangeStateEvent : GameEvent
    {
        public GameStateType State => _state;

        protected GameStateType _state;

        public ChangeStateEvent()
        {
            _name = "Change game state";
        }

        protected void SetupStateType(GameStateType state)
        {
            _state = state;

            _description = $"New state is {_state}";
        }
    }
}