using Events;
using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private Enemy _prefab;

        [SerializeField]
        private int _initPoolSize = 10;

        [SerializeField]
        private float[] _spawnX = { -2, 2 };

        [SerializeField]
        private float[] _spawnZ = { -2, 2 };

        [SerializeField]
        private float[] _spawnPeriod = { 10f, 20f };

        private Pool<Enemy> _pool;

        private int _spawnCount = 0;

        private readonly string _nameStart = "Enemy-";

        private void Awake()
        {
            _pool = new Pool<Enemy>(_prefab, _initPoolSize, transform);
        }

        private void Start()
        {
            StartCoroutine(SpawnProcess());
        }

        public void SpawnEnemy()
        {
            if (!GameSingleton.Instance.GameManager.IsPaused)
            {
                var position = new Vector3(
                    RandomExtensions.RangeArray(_spawnX),
                    0,
                    RandomExtensions.RangeArray(_spawnZ));

                var enemy = _pool.Spawn(transform);

                enemy.transform.position = position;

                enemy.OnDestroyed += Pool;

                _spawnCount++;

                enemy.Name = $"{_nameStart}{_spawnCount}";

                var e = new EnemyAppearedEvent(DateTime.Now, $"Enemy {enemy.Name} appeared at x: {position.x}, z: {position.z}", position);

                GameSingleton.Instance.EventManager.TriggerEvent(e);
            }
        }

        private void Pool(Enemy enemy)
        {
            _pool.Unspawn(enemy);

            enemy.OnDestroyed -= Pool;

            enemy.ResetEnemy();
        }

        private IEnumerator SpawnProcess()
        {
            while (gameObject.activeSelf)
            {
                SpawnEnemy();

                yield return new WaitForSeconds(RandomExtensions.RangeArray(_spawnPeriod));
            }
        }
    }
}