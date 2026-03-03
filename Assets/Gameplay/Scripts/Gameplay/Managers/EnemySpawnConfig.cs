using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig",
        menuName = "Configs/EnemySpawn")]
    public sealed class EnemySpawnConfig : ScriptableObject
    {
        public int PoolInitCount => _poolInitCount;

        public float SpawnPeriod => _spawnPeriod;

        [SerializeField]
        private float _spawnPeriod = 5f;

        [SerializeField]
        private int _poolInitCount = 10;

        [SerializeField]
        private float[] _xSpawnRange = new float[2];

        [SerializeField]
        private float[] _ySpawnRange = new float[2];

        public Vector3 GetSpawnPos()
        {
            return new Vector3(
                Random.Range(_xSpawnRange[0], _xSpawnRange[1]),
                Random.Range(_ySpawnRange[0], _ySpawnRange[1]),
                0);
        }
    }
}