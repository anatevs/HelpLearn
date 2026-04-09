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