using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig",
        menuName = "Configs/EnemySpawn")]
    public sealed class EnemySpawnConfig : ScriptableObject
    {
        public float WavePeriod => _wavePeriod;

        public int PoolInitCount => _poolInitCount;

        public float SpawnPeriod => _spawnPeriod;

        public EnemyWaveConfig[] WaveConfigs => _waveConfigs;

        [SerializeField]
        private float _wavePeriod = 5.0f;

        [SerializeField]
        private int _poolInitCount = 10;

        [SerializeField]
        private float _spawnPeriod = 0.2f;

        [SerializeField]
        private EnemyWaveConfig[] _waveConfigs;

        [SerializeField]
        private float _xSpawnRange;

        [SerializeField]
        private float _zSpawnRange;
    }
}