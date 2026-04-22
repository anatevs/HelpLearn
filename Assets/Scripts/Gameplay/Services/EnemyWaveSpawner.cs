using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EnemyWaveSpawner
    {
        private readonly WaveConfigInfo[] _waveEnemyInfo;
        private readonly EnemyWaveConfig _waveConfig;
        private readonly List<string> _names = new();
        private readonly Dictionary<string, int> _counters = new();
        private readonly Dictionary<string, Enemy> _prefabs = new();

        public EnemyWaveSpawner(EnemyWaveConfig enemyWaveConfig)
        {
            _waveConfig = enemyWaveConfig;
            _waveEnemyInfo = enemyWaveConfig.WaveInfo;

            foreach (var info in _waveEnemyInfo)
            {
                _names.Add(info.Prefab.Config.Name);
                _counters.Add(info.Prefab.Config.Name, info.Count);
                _prefabs.Add(info.Prefab.Config.Name, info.Prefab);
            }
        }

        public bool TryGetRandomEnemy(Transform spawnedTransform, out Enemy enemy)
        {
            enemy = null;

            if (!TryPopRandomName(out var name))
            {
                return false;
            }

            var prefab = _prefabs[name];

            enemy = Spawn(prefab, spawnedTransform);

            enemy.transform.position = _waveConfig.GetRandomLocation().position;

            return true;
        }

        public bool TryGetRandomEnemy(Dictionary<string, Pool<Enemy>> pools, Transform spawnedTransform, out Enemy enemy)
        {
            enemy = null;

            if (!TryPopRandomName(out var name))
            {
                return false;
            }

            enemy = Spawn(pools[name], spawnedTransform);

            enemy.transform.position = _waveConfig.GetRandomLocation().position;

            return true;
        }

        private bool TryPopRandomName(out string name)
        {
            name = null;

            if (_names.Count == 0)
            {
                return false;
            }

            name = _names[Random.Range(0, _names.Count)];

            _counters[name]--;

            if (_counters[name] == 0)
            {
                _names.Remove(name);
                _counters.Remove(name);
            }

            return true;
        }

        private Enemy Spawn(Enemy prefab, Transform spawnedTransform)
        {
            var enemy = GameObject.Instantiate(prefab, spawnedTransform);
            enemy.gameObject.SetActive(false);
            return enemy;
        }

        private Enemy Spawn(Pool<Enemy> pool, Transform spawnedTransform)
        {
            var enemy = pool.Spawn(spawnedTransform);
            enemy.gameObject.SetActive(false);
            return enemy;
        }
    }
}