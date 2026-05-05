using GameManagement;
using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public sealed class ItemsSpawner : MonoBehaviour,
        IResetable
    {
        public event Action<IItem> OnSpawned;

        [SerializeField]
        private ItemsSpawnConfig _config;

        [SerializeField]
        private Transform _spawnedParent;

        private Coroutine _spawnEnumerator;

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

            _spawnEnumerator = StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                var prefab = _config.GetPrefab(GetNextName());
                var position = _config.GetRandomPosition();

                var item = Instantiate(prefab, position, Quaternion.identity, _spawnedParent);

                OnSpawned?.Invoke(item);

                yield return _config.SpawnWait;
            }
        }

        private string GetNextName()
        {
            var index = UnityEngine.Random.Range(0, _config.ItemNames.Length);

            return _config.ItemNames[index];
        }
    }
}