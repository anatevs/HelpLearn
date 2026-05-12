using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class SwitchInputView : MonoBehaviour,
        ISwitchInputView
    {
        public event Action OnInputSwitched;

        [SerializeField]
        private Button _inputSwitchButton;

        [SerializeField]
        private TMP_Text _currentName;

        private void OnEnable()
        {
            _inputSwitchButton.onClick.AddListener(HandleSwitchInput);
        }

        private void OnDisable()
        {
            _inputSwitchButton.onClick.RemoveListener(HandleSwitchInput);
        }

        public void SetType(string typeName)
        {
            _currentName.text = typeName;
        }

        private void HandleSwitchInput()
        {
            OnInputSwitched?.Invoke();
        }
    }
}