using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig",
        menuName = "Configs/EnemySpawn")]
    public sealed class EnemySpawnConfig : ScriptableObject
    {
        public int PoolInitCount => _poolInitCount;

        public float WavePeriod => _wavePeriod;

        public float SpawnPeriod => _spawnPeriod;

        public int WaveSize => _waveSize;

        [SerializeField]
        private float _wavePeriod = 5.0f;

        [SerializeField]
        private float _spawnPeriod = 0.2f;

        [SerializeField]
        private int _waveSize = 10;

        [SerializeField]
        private int _poolInitCount = 10;

        [SerializeField]
        private Transform[] _spawnLocations;

        [SerializeField]
        private float _xSpawnRange;

        [SerializeField]
        private float _zSpawnRange;

        public Vector3 GetSpawnPos()
        {
            var shift = new Vector3(
            Random.Range(-_xSpawnRange, _xSpawnRange),
            0,
            Random.Range(-_zSpawnRange, _zSpawnRange));

            return _spawnLocations[Random.Range(0, _spawnLocations.Length)].position
                + shift;
        }
    }
}