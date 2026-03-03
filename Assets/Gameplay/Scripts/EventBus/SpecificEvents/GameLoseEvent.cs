namespace EventBusNamespace
{
    public class GameLoseEvent : ChangeStateEvent
    {
        public GameLoseEvent() : base()
        {
            SetupStateType(GameManagement.GameStateType.Lose);
        }
    }
}