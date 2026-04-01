using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyPrefabsConfig _prefabsConfig;

        private string[] _enemyNames;

        private readonly Dictionary<string, Enemy> _prefabs = new();

        public void Init()
        {
            _enemyNames = new string[_prefabsConfig.Prefabs.Length];

            for (int i = 0; i < _prefabsConfig.Prefabs.Length; i++)
            {
                var enemy = _prefabsConfig.Prefabs[i];

                _prefabs.Add(enemy.Config.Name, enemy);

                _enemyNames[i] = enemy.Config.Name;
            }
        }

        public Enemy GetRandomEnemy(Transform spawnedTransform)
        {
            var name = _enemyNames[Random.Range(0, _enemyNames.Length)];

            return Spawn(_prefabs[name], spawnedTransform);
        }

        private Enemy Spawn(Enemy prefab, Transform spawnedTransform)
        {
            var enemy = Instantiate(prefab, spawnedTransform);
            enemy.gameObject.SetActive(false);
            return enemy;
        }
    }
}