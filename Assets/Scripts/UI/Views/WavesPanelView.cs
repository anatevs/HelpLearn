using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class WavesPanelView : MonoBehaviour
    {
        [SerializeField]
        private WaveInfoView _waveViewPrefab;

        [SerializeField]
        private Transform _viewsTransform;

        [SerializeField]
        private Slider _nextWaveSlider;

        [SerializeField]
        private GameObject _nextWaitGO;

        [SerializeField]
        private Image _background;

        private readonly List<WaveInfoView> _views = new();

        public void AddWaveView(string number, string killed, string score)
        {
            var view = Instantiate(_waveViewPrefab, _viewsTransform);
            view.SetupView(number, killed, score);

            _views.Add(view);
        }

        public void ResetLevel()
        {
            foreach (var view in _views)
            {
                Destroy(view.gameObject);
            }

            _views.Clear();

            Hide();
        }

        public void Show(bool isGameEnd)
        {
            gameObject.SetActive(true);

            _background.enabled = !isGameEnd;
            _nextWaitGO.SetActive(!isGameEnd);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetNextWaiting(float portion)
        {
            _nextWaveSlider.value = portion;
        }
    }
}