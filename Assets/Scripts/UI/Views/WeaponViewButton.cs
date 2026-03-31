using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class WeaponViewButton : MonoBehaviour
    {
        public event Action OnClicked;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TMP_Text _name;

        [SerializeField]
        private TMP_Text _capacity;

        private float _sliderRange;

        private void Awake()
        {
            _sliderRange = _slider.maxValue - _slider.minValue;

            _slider.value = _slider.maxValue;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleClick);
        }

        public void SetName(string name)
        {
            _name.text = name;
        }

        public void SetCapacity(string charge)
        {
            _capacity.text = charge;
        }

        public void SetReadiness(float portion)
        {
            _slider.value = _sliderRange * portion;
        }

        public void SetInactive()
        {
            _slider.value = _slider.maxValue;
            _button.enabled = false;
        }

        public void SetSelected()
        {
            _button.Select();
        }

        private void HandleClick()
        {
            OnClicked?.Invoke();
        }
    }
}