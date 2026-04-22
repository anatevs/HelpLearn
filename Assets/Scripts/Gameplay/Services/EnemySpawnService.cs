using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class EnemySpawnService : MonoBehaviour
    {
        public event Action<Enemy> OnEnemySpawned;

        public event Action<Enemy> OnEnemyKilled;

        public EnemySpawnConfig Config => _config;

        [SerializeField]
        private EnemySpawnConfig _config;

        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _enemiesTransform;

        private Player _player;

        private EnemyWaveSpawner _waveSpawner;

        private WaitForSeconds _spawnWait;

        private Coroutine _spawnCoroutine;

        private readonly Dictionary<string, Pool<Enemy>> _pools = new();

        public void Construct(Player player)
        {
            _player = player;

            _spawnWait = new WaitForSeconds(_config.SpawnPeriod);
        }

        public void ResetLevel()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }

            if (_enemiesTransform.childCount > 0)
            {
                var activeEnemies = _enemiesTransform.GetComponentsInChildren<Enemy>();

                foreach (var enemy in activeEnemies)
                {
                    Unspawn(enemy);
                }
            }
        }

        private void OnDisable()
        {
            if (_enemiesTransform.childCount > 0)
            {
                var activeEnemies = _enemiesTransform.GetComponentsInChildren<Enemy>();

                foreach (var enemy in activeEnemies)
                {
                    enemy.OnKilled -= HandleEnemyKill;
                }
            }
        }

        private void Unspawn(Enemy enemy)
        {
            enemy.OnKilled -= HandleEnemyKill;

            enemy.transform.position = Vector3.zero;

            _pools[enemy.Config.Name].Unspawn(enemy);
        }

        public void SpawnEnemies(EnemyWaveConfig waveConfig)
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine(waveConfig));
        }

        private IEnumerator SpawnCoroutine(EnemyWaveConfig waveConfig)
        {
            _waveSpawner = new(waveConfig);

            foreach (var info in waveConfig.WaveInfo)
            {
                _pools.TryAdd(info.Prefab.Config.Name,
                    new Pool<Enemy>(info.Prefab, _config.PoolInitCount, _poolTransform));
            }

            while (TrySpawnRandom())
            {
                yield return _spawnWait;
            }

            yield return null;
        }

        private bool TrySpawnRandom()
        {
            if (!_waveSpawner.TryGetRandomEnemy(_pools, _enemiesTransform, out var enemy))
            {
                return false;
            }

            enemy.Init(_player);

            enemy.gameObject.SetActive(true);

            enemy.OnKilled += HandleEnemyKill;

            OnEnemySpawned?.Invoke(enemy);

            return true;
        }

        private void HandleEnemyKill(Enemy enemy)
        {
            Unspawn(enemy);

            OnEnemyKilled?.Invoke(enemy);
        }
    }
}