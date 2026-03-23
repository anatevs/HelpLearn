using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemyPrefabsConfig",
        menuName = "Configs/EnemyPrefabs")]
    public class EnemyPrefabsConfig : ScriptableObject
    {
        public Enemy[] Prefabs => _prefabs;

        [SerializeField]
        private Enemy[] _prefabs;
    }
}