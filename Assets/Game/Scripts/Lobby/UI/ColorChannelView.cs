using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.UI
{
    public class ColorChannelView : MonoBehaviour
    {
        public event Action OnValueChanged;

        public float Value
        {
            get => _slider.value;
            set => _slider.value = value;
        }

        [SerializeField]
        private TMP_Text _name;

        [SerializeField]
        private Slider _slider;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(HandleSliderChange);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(HandleSliderChange);
        }

        private void HandleSliderChange(float _)
        {
            OnValueChanged?.Invoke();
        }
    }
}