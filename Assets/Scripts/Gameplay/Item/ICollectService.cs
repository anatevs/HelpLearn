using GameManagement;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay
{
    public interface ICollectService : IResetable
    {
        public event Action<string, int> OnChanged;

        public event Action<ItemConfig, int> OnNewAdded;

        public event Action<string> OnRemoved;

        public IReadOnlyList<string> Names { get; }

        public ItemConfig GetItemConfig(string name);
        public int GeAmount(string name);

        public void AddItem(ItemConfig config);

        public void AddNewItem(ItemConfig config, int amount);

        public bool TryTakeItem(string Name, int amount);
    }
}