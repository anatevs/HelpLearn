using System;
using UnityEngine;

namespace Events
{
    public class EnemyAppearedEvent : GameEventT<Vector3>
    {
        public EnemyAppearedEvent(DateTime time, string description, Vector3 eventParam) :
            base(time, description, eventParam)
        {
            _type = EventType.EnemyAppeared;
        }
    }
}