using System;

namespace Gameplay
{
    public sealed class Wave
    {
        public event Action<WaveCompletedInfo> OnWaveEnded;

        public readonly int WaveSize;

        private WaveCompletedInfo _waveInfo = new();

        public Wave(int id, int waveSize)
        {
            _waveInfo.Id = id;
            WaveSize = waveSize;

            _waveInfo.Score = 0;
            _waveInfo.KilledCount = 0;
        }

        public void AddKilledEnemy(int reward)
        {
            _waveInfo.KilledCount++;

            _waveInfo.Score += reward;

            if (_waveInfo.KilledCount == WaveSize)
            {
                OnWaveEnded?.Invoke(_waveInfo);
            }
        }
    }

    public struct WaveCompletedInfo
    {
        public int Id;

        public int KilledCount;

        public int Score;
    }
}