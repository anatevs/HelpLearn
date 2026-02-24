using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ItemServiceConfig",
        menuName = "Configs/ItemService")]
    public class ItemServiceConfig : ScriptableObject
    {
        public Item[] Prefabs => _prefabs;
        public float[] XRange => _xRange;
        public float[] YRange => _yRange;
        public float SpawnPeriod => _spawnPeriod;
        public int InitPoolCount => _initPoolCount;

        [SerializeField]
        private Item[] _prefabs;

        [SerializeField]
        private float[] _xRange = new float[2];

        [SerializeField]
        private float[] _yRange = new float[2];

        [SerializeField]
        private float _spawnPeriod = 10f;

        [SerializeField]
        private int _initPoolCount = 10;
    }
}