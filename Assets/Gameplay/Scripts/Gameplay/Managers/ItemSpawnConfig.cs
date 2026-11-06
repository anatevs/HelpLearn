using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ItemsSpawnConfig",
        menuName = "Configs/ItemsSpawn")]
    public sealed class ItemSpawnConfig : ScriptableObject
    {
        public ItemConfig[] Configs => _configs;
        public float[] XRange => _xRange;
        public float[] YRange => _yRange;
        public float SpawnPeriod => _spawnPeriod;

        [SerializeField]
        private ItemConfig[] _configs;

        [SerializeField]
        private float[] _xRange = new float[2];

        [SerializeField]
        private float[] _yRange = new float[2];

        [SerializeField]
        private float _spawnPeriod = 10f;
    }
}