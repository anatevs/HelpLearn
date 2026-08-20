using System;
using System.Collections;
using UnityEngine;

namespace GameTest
{
    public class PerformanceCounter : MonoBehaviour
    {
        public event Action<float> OnFPSChanged;

        public event Action<float> OnDurationChanged;

        [SerializeField]
        private float _fpsAverageInterval = 1f;

        [SerializeField]
        private int _durationAverageCount = 10;

        private float _fps;

        private float _frameDuration;

        private float _fpsTimeCount;

        private int _fpsFrameCount;

        private float _durationTimeCount;
        private int _durationFrameCount;

        private void Update()
        {
            _fpsFrameCount++;
            _fpsTimeCount += Time.unscaledDeltaTime;

            if (_fpsTimeCount >= _fpsAverageInterval)
            {
                _fps = _fpsFrameCount / _fpsTimeCount;

                OnFPSChanged?.Invoke(_fps);

                _fpsTimeCount = 0;
                _fpsFrameCount = 0;
            }

            _durationFrameCount++;
            _durationTimeCount += Time.unscaledDeltaTime;

            if (_durationFrameCount >= _durationAverageCount)
            {
                _frameDuration = _durationTimeCount / _durationFrameCount;

                OnDurationChanged?.Invoke(_frameDuration);

                _durationTimeCount = 0;
                _durationFrameCount = 0;
            }
        }
    }
}