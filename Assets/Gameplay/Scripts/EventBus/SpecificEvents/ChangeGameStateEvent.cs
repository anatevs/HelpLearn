using GameManagement;

namespace EventBusNamespace
{
    public class ChangeGameStateEvent : GameEventT<GameState>
    {
        public ChangeGameStateEvent(GameState value) : base(value)
        {
            _name = "Change game state";
            _description = $"New state is {value}";
        }
    }
}