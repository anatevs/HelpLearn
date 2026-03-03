using GameManagement;

namespace EventBusNamespace
{
    public sealed class ChangeGameStateEvent : GameEventT<GameStateType>
    {
        public ChangeGameStateEvent(GameStateType value) : base(value)
        {
            _name = "Change game state";
            _description = $"New state is {value}";
        }
    }
}