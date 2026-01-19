using UnityEngine;

namespace Events
{
    public class ItemPickedEvent : GameEventT<GameObject>
    {
        public ItemPickedEvent(float time, string description, GameObject eventParam) :
            base(time, description, eventParam)
        {
            _type = EventType.ItemPicked;
        }
    }
}