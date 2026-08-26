using GameTest;
using UnityEngine;

namespace UI
{
    public class PerformancePresenter : MonoBehaviour
    {
        [SerializeField]
        private FPSCounter _fpsCounter;

        [SerializeField]
        private PerformanceView _view;

        private SpawnCounterService _spawnCounterService;

        public void Init(SpawnCounterService spawnCounterService)
        {
            _spawnCounterService = spawnCounterService;

            _spawnCounterService.OnPeakCountRaised += HandlePeakCountRaise;
        }

        private void OnEnable()
        {
            _fpsCounter.OnFPSUpdated += HandleFPSChange;
        }

        private void OnDisable()
        {
            _fpsCounter.OnFPSUpdated -= HandleFPSChange;

            if (_spawnCounterService != null)
            {
                _spawnCounterService.OnPeakCountRaised -= HandlePeakCountRaise;
            }
        }

        private void HandleFPSChange(float fps)
        {
            _view.SetFPS(fps.ToString("0"));

            HandleDurationChange(1 / fps);
        }

        private void HandleDurationChange(float duration)
        {
            _view.SetFrameDuration($"{duration * 1000:F1}");
        }

        private void HandlePeakCountRaise(int peakCount)
        {
            _view.SetPeakCount(peakCount.ToString());
        }
    }
}