using UnityEngine;

namespace Events
{
    public class EnemySpottedEvent : GameEventT<Vector3>
    {
        public EnemySpottedEvent(float time, string description, Vector3 eventParam) :
            base(time, description, eventParam)
        {
            _type = EventType.EnemySpotted;
        }
    }
}