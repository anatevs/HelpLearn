using Gameplay;
using System;

namespace UI
{
    public sealed class WavesPanelController 
        : IDisposable
    {
        private readonly WavesManager _manager;
        private readonly WavesPanelView _view;

        public WavesPanelController(WavesManager wavesManager,
            WavesPanelView view)
        {
            _manager = wavesManager;
            _view = view;

            _manager.OnWaveCompleted += AddWaveInfo;
        }

        void IDisposable.Dispose()
        {
            _manager.OnWaveCompleted -= AddWaveInfo;

            _manager.OnWaitPortionChanged -= _view.SetNextWaiting;
            _manager.OnWaveStarted -= StopNextWaiting;
        }

        private void AddWaveInfo(WaveCompletedInfo info, bool areWavesEnded)
        {
            _view.AddWaveView(info.Id.ToString(), 
                info.KilledCount.ToString(), info.Score.ToString());

            _view.Show(areWavesEnded);

            if (!areWavesEnded)
            {
                _manager.OnWaitPortionChanged += _view.SetNextWaiting;
                _manager.OnWaveStarted += StopNextWaiting;
            }
        }

        private void StopNextWaiting()
        {
            _manager.OnWaitPortionChanged -= _view.SetNextWaiting;
            _manager.OnWaveStarted -= StopNextWaiting;

            _view.Hide();
            _view.SetNextWaiting(0);
        }
    }
}