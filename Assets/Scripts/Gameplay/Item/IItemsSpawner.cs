using GameManagement;
using System;

namespace Gameplay
{
    public interface IItemsSpawner :
        IResetable
    {
        public event Action<IItem> OnSpawned;

        public void Init(IItemFactory itemFactory);

        public void MultiplySpawnPeriod(float multiplier);
    }
}