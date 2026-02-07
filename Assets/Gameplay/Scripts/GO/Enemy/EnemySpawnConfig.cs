using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig",
        menuName = "Configs/EnemySpawn")]
    public class EnemySpawnConfig : ScriptableObject
    {
        public Enemy[] Prefabs => _prefabs;

        public int PoolInitCount => _poolInitCount;

        [SerializeField]
        private Enemy[] _prefabs;

        [SerializeField]
        private int _poolInitCount = 10;
    }
}