using System;
using UnityEngine;

namespace Events
{
    public class ItemPickedEvent : GameEventT<GameObject>
    {
        public ItemPickedEvent(DateTime time, string description, GameObject eventParam) :
            base(time, description, eventParam)
        {
            _type = EventType.ItemPicked;
        }
    }
}