using Events;
using System;
using UnityEngine;
using System.Collections;
using System.Xml.Linq;

namespace Gameplay
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField]
        private Item _prefab;

        [SerializeField]
        private int _initPoolSize = 10;

        [SerializeField]
        private float[] _spawnX = { -10, 10 };

        [SerializeField]
        private float[] _spawnZ = { -10, 10 };

        [SerializeField]
        private float[] _spawnPeriod = { 5f, 10f };

        private Pool<Item> _pool;

        private int _spawnCount = 0;

        private readonly string _nameStart = "Item-";

        private void Awake()
        {
            _pool = new Pool<Item>(_prefab, _initPoolSize, transform);
        }

        private void Start()
        {
            StartCoroutine(SpawnProcess());
        }

        public void SpawnItem()
        {
            if (!GameSingleton.Instance.GameManager.IsPaused)
            {
                var position = new Vector3(
                    RandomExtensions.RangeArray(_spawnX),
                    0,
                    RandomExtensions.RangeArray(_spawnZ));

                var item = _pool.Spawn(transform);

                item.transform.position = position;

                item.OnPicked += Pool;

                _spawnCount++;

                item.Name = $"{_nameStart}{_spawnCount}";
            }
        }

        private void Pool(Item item)
        {
            _pool.Unspawn(item);

            item.OnPicked -= Pool;

            var e = new ItemPickedEvent(DateTime.Now, $"Item {item.Name} has been picked", item);
            GameSingleton.Instance.EventManager.TriggerEvent(e);
        }

        private IEnumerator SpawnProcess()
        {
            while (gameObject.activeSelf)
            {
                SpawnItem();

                yield return new WaitForSeconds(RandomExtensions.RangeArray(_spawnPeriod));
            }
        }
    }
}