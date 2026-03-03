namespace EventBusNamespace
{
    public class GameWinEvent : ChangeStateEvent
    {
        public GameWinEvent() : base()
        {
            SetupStateType(GameManagement.GameStateType.Win);
        }
    }
}