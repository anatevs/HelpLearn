using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public sealed class EnemySpawnService : MonoBehaviour
    {
        public event Action<int> OnEnemyKilled;

        public EnemySpawnConfig Config => _config;

        [SerializeField]
        private EnemySpawnConfig _config;

        [SerializeField]
        private Transform _enemiesTransform;

        private Player _player;

        private EnemyWaveSpawner _waveSpawner;

        private WaitForSeconds _spawnWait;

        private Coroutine _spawnCoroutine;

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

        private void Unspawn(Enemy enemy)
        {
            enemy.OnKilled -= HandleEnemyKill;

            enemy.transform.position = Vector3.zero;

            UnspawnEnemy(enemy);
        }

        private void UnspawnEnemy(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }

        public void SpawnEnemies(EnemyWaveConfig waveConfig)
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine(waveConfig));
        }

        private IEnumerator SpawnCoroutine(EnemyWaveConfig waveConfig)
        {
            _waveSpawner = new(waveConfig);

            while (TrySpawnRandom())
            {
                yield return _spawnWait;
            }

            yield return null;
        }

        private bool TrySpawnRandom()
        {
            if (!_waveSpawner.TryGetRandomEnemy(_enemiesTransform, out var enemy))
            {
                return false;
            }

            enemy.Init(_player);

            enemy.gameObject.SetActive(true);

            enemy.OnKilled += HandleEnemyKill;

            return true;
        }

        private void HandleEnemyKill(Enemy enemy)
        {
            OnEnemyKilled?.Invoke(enemy.Config.KillReward);

            Unspawn(enemy);
        }
    }
}