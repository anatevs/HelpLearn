using UnityEngine;

namespace Gameplay
{
    public class ItemFactory :
        IItemFactory
    {
        private Transform _spawnedParent;

        private ItemsSpawnConfig _config;

        public void Init(Transform spawnedParent, ItemsSpawnConfig config)
        {
            _spawnedParent = spawnedParent;
            _config = config;
        }

        public IItem CreateNext()
        {
            var prefab = _config.GetPrefab(GetRandomName());
            var position = _config.GetRandomPosition();

            var item = GameObject.Instantiate(prefab, position, Quaternion.identity, _spawnedParent);

            return item;
        }

        private string GetRandomName()
        {
            var index = Random.Range(0, _config.ItemNames.Length);

            return _config.ItemNames[index];
        }
    }
}