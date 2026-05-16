using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public sealed class ItemsSpawner : MonoBehaviour,
        IItemsSpawner
    {
        public event Action<IItem> OnSpawned;

        [SerializeField]
        private ItemsSpawnConfig _config;

        [SerializeField]
        private Transform _spawnedParent;

        private Coroutine _spawnEnumerator;

        private WaitForSeconds _spawnWait;

        private float _spawnPeriod;

        public void Init()
        {
            _config.Init();
        }

        public void ResetLevel()
        {
            if (_spawnEnumerator != null)
            {
                StopCoroutine(_spawnEnumerator);
            }

            _spawnWait = _config.SpawnWait;
            _spawnPeriod = _config.SpawnPeriod;

            _spawnEnumerator = StartCoroutine(SpawnCoroutine());
        }

        public void MultiplySpawnPeriod(float multiplier)
        {
            _spawnPeriod *= multiplier;
            _spawnWait = new WaitForSeconds(_spawnPeriod);
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                yield return _spawnWait;

                var prefab = _config.GetPrefab(GetNextName());
                var position = _config.GetRandomPosition();

                var item = Instantiate(prefab, position, Quaternion.identity, _spawnedParent);

                OnSpawned?.Invoke(item);
            }
        }

        private string GetNextName()
        {
            var index = UnityEngine.Random.Range(0, _config.ItemNames.Length);

            return _config.ItemNames[index];
        }
    }
}