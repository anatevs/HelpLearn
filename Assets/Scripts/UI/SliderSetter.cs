using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class SliderSetter : MonoBehaviour
    {
        public event Action<float> ValueChanged;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TMP_Text _valueText;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void SetValue(float value)
        {
            _slider.value = value;
        }

        private void OnValueChanged(float value)
        {
            ValueChanged?.Invoke(value);

            _valueText.text = value.ToString();
        }
    }
}