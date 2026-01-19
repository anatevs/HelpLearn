namespace Events
{
    public class WeatherEvent : GameEventT<WeatherType>
    {
        public WeatherEvent(float time, string description, WeatherType eventParam) :
            base(time, description, eventParam)
        {
            _type = EventType.WeatherChanged;
        }
    }
}