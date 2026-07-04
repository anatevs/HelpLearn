using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class ItemSpawnPoint : MonoBehaviour
    {
        public event Action<ItemType> OnSpawnRequested;

        public ItemType ItemType => _itemConfig.Type;

        [SerializeField]
        private ItemConfig _itemConfig;

        private float _respawnDelay;

        public void Init(float respawnDelay)
        {
            _respawnDelay = respawnDelay;

            var rand = UnityEngine.Random.Range(0f, 1f);
        }

        public void SetItemToPoint(PickableItem item)
        {
            item.transform.SetPositionAndRotation(transform.position, transform.rotation);

            item.OnPicked += PickItem;
        }

        private IEnumerator SpawnItemsCoroutine()
        {
            var timer = 0f;

            while (timer < _respawnDelay)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            OnSpawnRequested?.Invoke(_itemConfig.Type);
        }

        private void PickItem(PickableItem item)
        {
            if (item != null)
            {
                item.OnPicked -= PickItem;
            }

            StartCoroutine(SpawnItemsCoroutine());
        }




        

        int GetRandomItemIndex(float[] itemWeights)
        {
            float totalWeight = CalculateTotalWeight(itemWeights);
            float randomPoint = UnityEngine.Random.Range(0, totalWeight);

            for (int i = 0; i < itemWeights.Length; i++)
            {
                if (randomPoint < itemWeights[i])
                {
                    return i;
                }
                randomPoint -= itemWeights[i];
            }
            return 0;
        }

        float CalculateTotalWeight(float[] itemWeights)
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