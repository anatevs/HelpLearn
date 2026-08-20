using GameTest;
using UnityEngine;

namespace UI
{
    public class PerformancePresenter : MonoBehaviour
    {
        [SerializeField]
        private PerformanceCounter _performanceCounter;

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
            _performanceCounter.OnFPSChanged += HandleFPSChange;
            _performanceCounter.OnDurationChanged += HandleDurationChange;
        }

        private void OnDisable()
        {
            _performanceCounter.OnFPSChanged -= HandleFPSChange;
            _performanceCounter.OnDurationChanged -= HandleDurationChange;

            if (_spawnCounterService != null)
            {
                _spawnCounterService.OnPeakCountRaised -= HandlePeakCountRaise;
            }
        }

        private void HandleFPSChange(float fps)
        {
            _view.SetFPS(fps.ToString("0"));
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