using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "WaveConfig",
        menuName = "Configs/Wave")]
    public sealed class EnemyWaveConfig : ScriptableObject
    {
        public WaveConfigInfo[] WaveInfo => _enemiesInfo;

        public float NextWaveDelay => _nextWaveDelay;

        [SerializeField]
        private WaveConfigInfo[] _enemiesInfo;

        [SerializeField]
        private float _nextWaveDelay;

        [SerializeField]
        private SpawnRoom[] _spawnRooms;

        private Transform[] _locations;

        public void Init()
        {
            int locationsAmount = 0;

            foreach (var room in _spawnRooms)
            {
                locationsAmount += room.Locations.Count;
            }

            _locations = new Transform[locationsAmount];

            int index = 0;
            foreach (var room in _spawnRooms)
            {
                foreach (var location in room.Locations)
                {
                    _locations[index] = location;
                    index++;
                }
            }
        }

        public Transform GetRandomLocation()
        {
            var index = Random.Range(0, _locations.Length);
            return _locations[index];
        }

        public int GetWaveSize()
        {
            int waveSize = 0;

            foreach (var info in _enemiesInfo)
            {
                waveSize += info.Count;
            }

            return waveSize;
        }
    }
}