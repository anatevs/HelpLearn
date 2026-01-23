using Gameplay;
using System;

namespace Events
{
    public class ItemPickedEvent : GameEventT<Item>
    {
        public ItemPickedEvent(DateTime time, string description, Item eventParam) :
            base(time, description, eventParam)
        {
            _type = EventType.ItemPicked;
        }
    }
}