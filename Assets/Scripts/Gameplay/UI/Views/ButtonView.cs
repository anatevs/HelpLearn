using UnityEngine;
using System;
using UnityEngine.UI;

namespace UI
{
    public sealed class ButtonView : MonoBehaviour,
        IButtonView
    {
        public event Action OnClicked;

        [SerializeField]
        private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            OnClicked?.Invoke();
        }
    }
}