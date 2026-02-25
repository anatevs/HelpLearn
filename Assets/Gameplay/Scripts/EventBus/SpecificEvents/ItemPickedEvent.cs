using Gameplay;

namespace EventBusNamespace
{
    public sealed class ItemPickedEvent : GameEventT<Item>
    {
        public ItemPickedEvent(Item item) : base(item)
        {
            _name = "Item picked";

            _description = $"Item name: {item.Config.Name}";
        }
    }
}