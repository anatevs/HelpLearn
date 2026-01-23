using Events;
using System;

namespace Gameplay
{
    public class ItemManager : SpawnableTimerManager<Item>
    {
        protected override void Awake()
        {
            base.Awake();

            _namePrefix = "Item-";
        }

        protected override void MakeAtUnspawn(Item item)
        {
            var e = new ItemPickedEvent(DateTime.Now, $"Item {item.Name} has been picked", item);
            GameSingleton.Instance.EventManager.TriggerEvent(e);
        }
    }
}