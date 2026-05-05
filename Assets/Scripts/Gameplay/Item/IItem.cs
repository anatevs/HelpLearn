using System;

namespace Gameplay
{
    public interface IItem
    {
        public event Action<IItem> OnCollected;

        public ItemConfig Config { get; }
        public string Name { get; }
        public int Amount { get; }

        public void Collect();

        public void Deactivate();
    }
}