using System;
using System.Collections;
using UnityEngine;

namespace GameTest
{
    public class FPSCounter : MonoBehaviour
    {
        public event Action<float> OnFPSUpdated;

        [SerializeField]
        private float _fpsAverageInterval = 1f;

        private float _fps;

        private float _timeCount;

        private int _framesCount;

        private void Start()
        {
            StartCoroutine(FPSCoroutine());
        }

        private void Update()
        {
            _framesCount++;
            _timeCount += Time.unscaledDeltaTime;
        }

        private IEnumerator FPSCoroutine()
        {
            var wait = new WaitForSeconds(_fpsAverageInterval);

            while (true)
            {
                yield return wait;

                _fps = _framesCount / _timeCount;

                OnFPSUpdated?.Invoke(_fps);

                _timeCount = 0;
                _framesCount = 0;
            }
        }
    }
}