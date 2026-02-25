namespace EventBusNamespace
{
    public sealed class RestartEvent : GameEvent
    {
        public RestartEvent()
        {
            _name = "Restart game";
            _description = "";
        }
    }
}