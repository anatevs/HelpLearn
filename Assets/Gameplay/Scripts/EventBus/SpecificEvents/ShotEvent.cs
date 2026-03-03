using Gameplay;

namespace EventBusNamespace
{
    public class ShotEvent : GameEventT<ShotComponent>
    {
        public ShotEvent(ShotComponent value) : base(value)
        {
            _name = "Shot";

            _description = $"From {value.gameObject.name}";
        }
    }
}