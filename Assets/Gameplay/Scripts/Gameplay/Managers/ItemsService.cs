using EventBusNamespace;
using GameManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ItemsService : DDOLClass<ItemsService>
    {
        [SerializeField]
        private ItemServiceConfig _config;

        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _activeItemsTransform;

        private readonly Dictionary<string, Pool<Item>> _pools = new();

        private string[] _itemNames;

        private WaitForSeconds _spawnWait;

        private (float[] x, float[] y) _posRange;

        private Coroutine _spawnCoroutine;

        private void OnDisable()
        {
            EventBus.Unsubscribe<ItemPickedEvent>(Unspawn);
        }

        public void Init()
        {
            Init(_config);
        }

        private void Init(ItemServiceConfig config)
        {
            _itemNames = new string[config.Prefabs.Length];

            for(int i = 0; i < config.Prefabs.Length; i++)
            {
                var prefab = config.Prefabs[i];

                _pools.Add(prefab.Config.Name, new Pool<Item>(prefab, config.InitPoolCount, _poolTransform));

                _itemNames[i] = prefab.Config.Name;
            }

            _spawnWait = new WaitForSeconds(config.SpawnPeriod);

            _posRange = (config.XRange, config.YRange);

            EventBus.Subscribe<ItemPickedEvent>(Unspawn);

            Reset();
        }

        public void Reset()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }

            if (_activeItemsTransform.childCount > 0)
            {
                var activeEnemies = _activeItemsTransform.GetComponentsInChildren<Item>();

                foreach (var item in activeEnemies)
                {
                    Unspawn(item);
                }
            }

            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (gameObject.activeSelf)
            {
                var pos = new Vector2(
                    Random.Range(_posRange.x[0], _posRange.x[1]),
                    Random.Range(_posRange.y[0], _posRange.y[1]));

                var item = _pools[_itemNames[Random.Range(0, _itemNames.Length)]].Spawn(_activeItemsTransform);

                item.transform.position = pos;

                item.gameObject.SetActive(true);

                yield return _spawnWait;
            }
        }

        private void Unspawn(ItemPickedEvent e)
        {
            var item = e.Value;

            Unspawn(item);
        }

        private void Unspawn(Item item)
        {
            item.gameObject.SetActive(false);

            item.transform.position = Vector3.zero;

            _pools[item.Config.Name].Unspawn(item);
        }
    }
}