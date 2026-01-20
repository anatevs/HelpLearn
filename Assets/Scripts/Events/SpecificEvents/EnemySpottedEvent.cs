using System;
using UnityEngine;

namespace Events
{
    public class EnemySpottedEvent : GameEventT<Vector3>
    {
        public EnemySpottedEvent(DateTime time, string description, Vector3 eventParam) :
            base(time, description, eventParam)
        {
            _type = EventType.EnemySpotted;
        }
    }
}