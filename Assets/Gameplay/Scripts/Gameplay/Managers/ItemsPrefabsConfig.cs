using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ItemsPrefabsConfig",
        menuName = "Configs/ItemsPrefabs")]
    public class ItemsPrefabsConfig : ScriptableObject
    {
        public Item[] Prefabs => _prefabs;

        [SerializeField]
        private Item[] _prefabs;
    }
}