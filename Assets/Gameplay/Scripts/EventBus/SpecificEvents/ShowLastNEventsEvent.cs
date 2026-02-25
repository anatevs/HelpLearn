namespace EventBusNamespace
{
    public class ShowLastNEventsEvent : GameEventT<int>
    {
        public ShowLastNEventsEvent(int value) : base(value)
        {
            _name = $"UI event: show last {value} events";

            _description = "";
        }
    }
}