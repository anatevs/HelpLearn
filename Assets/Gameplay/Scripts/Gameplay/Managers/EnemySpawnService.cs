using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EnemySpawnService : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnConfig _config;

        [SerializeField]
        private Transform _poolTransform;

        private Dictionary<string, Pool<Enemy>> _pools = new();

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            foreach (Enemy enemy in _config.Prefabs)
            {
                _pools.Add(enemy.Name, new Pool<Enemy>(enemy, _config.PoolInitCount, _poolTransform));
            }
        }
    }
}