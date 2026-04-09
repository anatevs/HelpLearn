using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemyPrefabsConfig",
        menuName = "Configs/EnemyPrefabs")]
    public class EnemyPrefabsConfig : ScriptableObject
    {
        [SerializeField]
        private Enemy[] _prefabs;

        private readonly Dictionary<string, Enemy> _prefabsDict = new();

        public void Init()
        {
            for (int i = 0; i < _prefabs.Length; i++)
            {
                var enemy = _prefabs[i];

                _prefabsDict.Add(enemy.Config.Name, enemy);
            }
        }

        public Enemy GetEnemyPrefab(string name)
        {
            return _prefabsDict[name];
        }
    }
}