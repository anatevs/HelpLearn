using GameManagement;

namespace EventBusNamespace
{
    public class GameInitEvent : ChangeStateEvent
    {
        public GameInitEvent() : base()
        {
            SetupStateType(GameStateType.Init);
        }
    }
}