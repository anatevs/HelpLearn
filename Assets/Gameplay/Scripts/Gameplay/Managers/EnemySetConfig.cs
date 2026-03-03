using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemySetConfig",
        menuName = "Configs/EnemySet")]
    public class EnemySetConfig : ScriptableObject
    {
        public Enemy[] Prefabs => _prefabs;

        [SerializeField]
        private Enemy[] _prefabs;
    }
}