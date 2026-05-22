using UnityEngine;

namespace Gameplay
{
    public interface IItemFactory
    {
        public void Init(Transform spawnedParent, ItemsSpawnConfig config);

        public IItem CreateNext();
    }
}