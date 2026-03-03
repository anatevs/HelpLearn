namespace EventBusNamespace
{
    public class GamePlayingEvent : ChangeStateEvent
    {
        public GamePlayingEvent() : base()
        {
            SetupStateType(GameManagement.GameStateType.Playing);
        }
    }
}