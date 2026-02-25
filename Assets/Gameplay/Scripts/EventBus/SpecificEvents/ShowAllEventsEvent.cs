namespace EventBusNamespace
{
    public sealed class ShowAllEventsEvent : GameEvent
    {
        public ShowAllEventsEvent()
        {
            _name = "UI event: show all events";
            _description = "";
        }
    }
}