using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class SwitchInputController : MonoBehaviour,
        ISwitchInputController
    {
        public event Action OnInputSwitched;

        [SerializeField]
        private Button _inputSwitchButton;

        private void OnEnable()
        {
            _inputSwitchButton.onClick.AddListener(HandleSwitchInput);
        }

        private void OnDisable()
        {
            _inputSwitchButton.onClick.RemoveListener(HandleSwitchInput);
        }

        private void HandleSwitchInput()
        {
            OnInputSwitched?.Invoke();
        }
    }
}