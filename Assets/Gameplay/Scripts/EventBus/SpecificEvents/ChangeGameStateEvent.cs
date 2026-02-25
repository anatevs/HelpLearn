using GameManagement;

namespace EventBusNamespace
{
    public sealed class ChangeGameStateEvent : GameEventT<GameState>
    {
        public ChangeGameStateEvent(GameState value) : base(value)
        {
            _name = "Change game state";
            _description = $"New state is {value}";
        }
    }
}