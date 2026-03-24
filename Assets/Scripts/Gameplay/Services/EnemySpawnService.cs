using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class EnemySpawnService : MonoBehaviour
    {
        public event Action<int> OnEnemyKilled;

        [SerializeField]
        private EnemySpawnConfig _config;

        [SerializeField]
        private EnemySpawner _spawner;

        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _enemiesTransform;

        private Player _player;

        private readonly Dictionary<string, Pool<Enemy>> _pools = new();

        private WaitForSeconds _waveWait;
        private WaitForSeconds _spawnWait;

        private Coroutine _wavesCoroutine;
        private Coroutine _spawnCoroutine;

        public void Construct(Player player)
        {
            _player = player;

            _spawner.Init(_pools, _config.PoolInitCount, _poolTransform);

            _waveWait = new WaitForSeconds(_config.WavePeriod);

            _spawnWait = new WaitForSeconds(_config.SpawnPeriod);
        }

        public void Init()
        {
            if (_wavesCoroutine != null)
            {
                StopCoroutine(_wavesCoroutine);
                _wavesCoroutine = null;
            }

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

            _wavesCoroutine = StartCoroutine(WavesSpawnCoroutine());
        }

        private void Unspawn(Enemy enemy)
        {
            enemy.OnKilled -= HandleEnemyKill;

            enemy.transform.position = Vector3.zero;

            _pools[enemy.Config.Name].Unspawn(enemy);
        }

        private IEnumerator WavesSpawnCoroutine()
        {
            while (gameObject.activeSelf)
            {
                yield return _spawnCoroutine = StartCoroutine(SpawnCoroutine());

                yield return _waveWait;
            }
        }

        private IEnumerator SpawnCoroutine()
        {
            for (int i = 0; i < _config.WaveSize; i++)
            {
                SpawnRandom();

                yield return _spawnWait;
            }

            yield return null;
        }

        private void SpawnRandom()
        {
            var enemy = _spawner.GetRandomEnemy(_pools, _enemiesTransform);
            enemy.Init(_player);

            var pos = _config.GetSpawnPos();

            enemy.transform.position = pos;

            enemy.gameObject.SetActive(true);

            enemy.OnKilled += HandleEnemyKill;
        }

        private void HandleEnemyKill(Enemy enemy)
        {
            OnEnemyKilled?.Invoke(enemy.Config.KillReward);

            Unspawn(enemy);
        }
    }
}