using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EnemySpawner
    {
        private readonly Dictionary<string, PoolWithFactory<Enemy>> _pools;

        private readonly Transform _spawnTransform;

        private string[] _enemyNames;

        public EnemySpawner(Dictionary<string, PoolWithFactory<Enemy>> pools, Transform activeTransform)
        {
            _pools = pools;
            _spawnTransform = activeTransform;
        }

        public void InitOnScene(EnemySpawnConfig spawnConfig)
        {
            _enemyNames = new string[spawnConfig.Prefabs.Length];

            for (int i = 0; i < spawnConfig.Prefabs.Length; i++)
            {
                _enemyNames[i] = spawnConfig.Prefabs[i].Name;
            }
        }

        public Enemy GetRandomEnemy()
        {
            var name = _enemyNames[Random.Range(0, _enemyNames.Length)];

            return _pools[name].Spawn(_spawnTransform);
        }
    }
}