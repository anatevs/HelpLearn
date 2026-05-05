using GameManagement;
using System;

namespace Gameplay
{
    public interface IItemsSceneService :
        IResetable,
        IDisposable
    {
        public event Action<ItemConfig> OnCollected;
    }
}