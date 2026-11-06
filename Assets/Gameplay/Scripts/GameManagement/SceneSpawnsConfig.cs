using Gameplay;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "SceneSpawnsConfig",
        menuName = "Configs/SceneSpawns")]
    public class SceneSpawnsConfig : ScriptableObject
    {
        public EnemySpawnConfig EnemySpawnConfig => _enemyConfig;
        public ItemSpawnConfig ItemSpawnConfig => _itemConfig;
        public ProjectileSpawnConfig ProjectileSpawnConfig => _projectileConfig;

        [SerializeField]
        private EnemySpawnConfig _enemyConfig;

        [SerializeField]
        private ItemSpawnConfig _itemConfig;

        [SerializeField]
        private ProjectileSpawnConfig _projectileConfig;
    }
}