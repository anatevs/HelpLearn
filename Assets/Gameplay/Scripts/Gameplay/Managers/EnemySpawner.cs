using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemySetConfig _setConfig;

        private string[] _enemyNames;

        public void Init(Dictionary<string, Pool<Enemy>> pools, int poolInitCount, Transform poolTransform)
        {
            _enemyNames = new string[_setConfig.Prefabs.Length];

            for (int i = 0; i < _setConfig.Prefabs.Length; i++)
            {
                var enemy = _setConfig.Prefabs[i];

                pools.Add(enemy.Config.Name, new Pool<Enemy>(enemy, poolInitCount, poolTransform));

                _enemyNames[i] = enemy.Config.Name;
            }
        }

        public Enemy GetRandomEnemy(Dictionary<string, Pool<Enemy>> pools, Transform spawnedTransform)
        {
            var name = _enemyNames[Random.Range(0, _enemyNames.Length)];

            return pools[name].Spawn(spawnedTransform);
        }
    }
}