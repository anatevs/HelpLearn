using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ItemSpawnPoint : MonoBehaviour
    {
        public event Action<ItemType> OnSpawnRequested;

        public event Action<string> OnSpawnNameRequested;

        public event Action<ItemSpawnPoint> OnItemPicked;

        public ItemType ItemType => _itemType;

        [SerializeField]
        private ItemType _itemType;

        private List<ItemSpawnData> _groupItemData;

        private float[] _groupWeights;

        private WaitForSeconds _respawnWait;

        public void Init(List<ItemSpawnData> groupItemData)
        {
            _groupItemData = groupItemData;

            _respawnWait = new WaitForSeconds(groupItemData[0].RespawnDelay);

            _groupWeights = new float[groupItemData.Count];

            for (int i = 0; i < groupItemData.Count; i++)
            {
                _groupWeights[i] = groupItemData[i].SpawnWeightRate;
            }
        }

        public void SetItemToPoint(PickableItem item)
        {
            item.transform.SetPositionAndRotation(transform.position, transform.rotation);

            item.OnPicked += PickItem;
        }

        private IEnumerator SpawnItemsCoroutine()
        {
            yield return _respawnWait;

            if (_groupWeights.Length == 1)
            {
                OnSpawnRequested?.Invoke(_itemType);

                Debug.Log($"ordinary point spawn request for {_itemType}");
            }
            else if (_groupWeights.Length > 1)
            {
                var index = GetRandomItemIndex(_groupWeights);

                var itemName = _groupItemData[index].Config.Name;

                OnSpawnNameRequested?.Invoke(itemName);
            }
        }

        private void PickItem(PickableItem item)
        {
            if (item != null)
            {
                item.OnPicked -= PickItem;
                OnItemPicked?.Invoke(this);
            }

            StartCoroutine(SpawnItemsCoroutine());
        }

        private int GetRandomItemIndex(float[] itemWeights)
        {
            float totalWeight = CalculateTotalWeight(itemWeights);
            float randomValue = UnityEngine.Random.Range(0, totalWeight);

            for (int i = 0; i < itemWeights.Length; i++)
            {
                if (randomValue < itemWeights[i])
                {
                    return i;
                }
                randomValue -= itemWeights[i];
            }
            return 0;
        }

        private float CalculateTotalWeight(float[] itemWeights)
        {
            float total = 0;

            foreach (float weight in itemWeights)
            {
                total += weight;
            }
            return total;
        }
    }
}