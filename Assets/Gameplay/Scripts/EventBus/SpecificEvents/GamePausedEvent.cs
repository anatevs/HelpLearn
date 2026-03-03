namespace EventBusNamespace
{
    public class GamePausedEvent : ChangeStateEvent
    {
        public GamePausedEvent() : base()
        {
            SetupStateType(GameManagement.GameStateType.Paused);
        }
    }
}