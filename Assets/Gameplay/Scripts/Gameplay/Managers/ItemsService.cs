using EventBusNamespace;
using GameManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public sealed class ItemsService : MonoBehaviour,
        IResetable
    {
        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _activeItemsTransform;

        [SerializeField]
        private ItemSpawnConfig _config;

        [SerializeField]
        private int _poolInitCount = 10;

        private EventBus _eventBus;

        private readonly Dictionary<string, PoolWithFactory<Item>> _pools = new();

        private string[] _itemNames;

        private WaitForSeconds _spawnWait;

        private (float[] x, float[] y) _posRange;

        private Coroutine _spawnCoroutine;


        [Inject]
        public void Construct(EventBus eventBus, IObjectsFactory<Item> itemsFacrory)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<ItemPickedEvent>(Unspawn);

            foreach (var itemName in itemsFacrory.Names)
            {
                _pools.Add(itemName, new PoolWithFactory<Item>(itemsFacrory, _poolInitCount, _poolTransform, itemName));
            }
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<ItemPickedEvent>(Unspawn);
        }

        public void InitOnScene(ItemSpawnConfig config)
        {
            _config = config;

            _itemNames = new string[config.Prefabs.Length];

            for(int i = 0; i < config.Prefabs.Length; i++)
            {
                var prefab = config.Prefabs[i];

                _itemNames[i] = prefab.Config.Name;
            }

            _spawnWait = new WaitForSeconds(config.SpawnPeriod);

            _posRange = (config.XRange, config.YRange);
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

                item.Construct(_eventBus);

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
            item.transform.position = Vector3.zero;

            _pools[item.Config.Name].Unspawn(item);
        }
    }
}