using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ItemsSpawnConfig",
        menuName = "Configs/ItemsSpawn")]
    public sealed class ItemSpawnConfig : ScriptableObject
    {
        public Item[] Prefabs => _prefabs;
        public float[] XRange => _xRange;
        public float[] YRange => _yRange;
        public float SpawnPeriod => _spawnPeriod;

        [SerializeField]
        private Item[] _prefabs;

        [SerializeField]
        private float[] _xRange = new float[2];

        [SerializeField]
        private float[] _yRange = new float[2];

        [SerializeField]
        private float _spawnPeriod = 10f;
    }
}