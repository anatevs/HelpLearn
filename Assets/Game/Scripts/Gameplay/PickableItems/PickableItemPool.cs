using UnityEngine;

namespace Gameplay
{
    public class PickableItemPool : ClientPool<PickableItem>
    {
        private ItemConfig _itemConfig;

        public PickableItemPool(PickableItem prefab, int initSize,
            Transform poolTransform, Transform spawnedTransform) 
            : base(prefab, initSize, poolTransform, spawnedTransform)
        {
        }

        public void InitItemConfig(ItemConfig itemConfig)
        {
            _itemConfig = itemConfig;
            _prefab = _itemConfig.PickablePrefab;
        }

        protected override void PrepareForSpawn(PickableItem item)
        {
            base.PrepareForSpawn(item);

            item.Init(_itemConfig);
        }
    }
}