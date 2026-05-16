using System;
using System.Collections.Generic;

namespace Gameplay
{
    public sealed class ItemsSceneService :
        IItemsSceneService
    {
        public event Action<ItemConfig> OnCollected;

        private readonly List<IItem> _items = new();

        private readonly IItemsSpawner _spawner;

        public ItemsSceneService(IItemsSpawner spawner)
        {
            _spawner = spawner;

            _spawner.OnSpawned += Add;
        }

        public void ResetLevel()
        {
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                ReleaseItem(i);
            }
        }

        void IDisposable.Dispose()
        {
            foreach (var item in _items)
            {
                item.OnCollected -= Collect;
            }

            _spawner.OnSpawned -= Add;
        }

        private void Add(IItem item)
        {
            _items.Add(item);

            item.OnCollected += Collect;
        }

        private void Collect(IItem item)
        {
            OnCollected?.Invoke(item.Config);

            var index = _items.IndexOf(item);

            ReleaseItem(index);
        }

        private void ReleaseItem(int index)
        {
            _items[index].OnCollected -= Collect;

            _items[index].Deactivate();

            _items.RemoveAt(index);
        }
    }
}