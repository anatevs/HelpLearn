using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponView : MonoBehaviour
    {
        public event Action OnClicked;

        [SerializeField]
        private Toggle _toggle;

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

            SetSliderMax();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(HandleClick);
        }

        public void SetToggleGroup(ToggleGroup group)
        {
            _toggle.group = group;
        }

        public void SetName(string name)
        {
            _name.text = name;
        }

        public void SetCapacity(string charge)
        {
            _capacity.text = charge;
            SetSliderMax();
        }

        public void SetReadiness(float portion)
        {
            _slider.value = _sliderRange * portion;
        }

        public void SetInactive()
        {
            SetSliderMax();
            _toggle.isOn = false;
            _toggle.interactable = false;
        }

        public void SetSelected()
        {
            _toggle.isOn = true;
        }

        private void SetSliderMax()
        {
            _slider.value = _slider.maxValue;
        }

        private void HandleClick(bool isOn)
        {
            if (isOn)
            {
                OnClicked?.Invoke();
            }
        }
    }
}