using GameManagement;
using System;
using System.Collections;
namespace Gameplay
{
    public interface ICollectService : IResetable
    {
        public event Action<string, int> OnChanged;

        public event Action<string, int> OnNewAdded;

        public event Action<string> OnRemoved;

        public void Init(ItemConfig[] configs);

        public void AddItem(ItemConfig config);

        public void AddNewItem(ItemConfig config, int amount);

        public bool TryTakeItem(string Name, int amount);
    }
}