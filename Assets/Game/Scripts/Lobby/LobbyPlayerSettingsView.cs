using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.UI
{
    public class LobbyPlayerSettingsView : MonoBehaviour,
        ILobbyPlayerSettingsView
    {
        public event Action<string> OnNameSet;
        public event Action<Color> OnColorSet;

        private Color _color = Color.white;

        [SerializeField]
        private TMP_InputField _nameField;

        [SerializeField]
        private Button _setColorButton;

        [SerializeField]
        private ColorChannelView _rChannel;

        [SerializeField]
        private ColorChannelView _gChannel;
        
        [SerializeField]
        private ColorChannelView _bChannel;

        [SerializeField]
        private Image _colorView;

        private void OnEnable()
        {
            _rChannel.OnValueChanged += SetColor;
            _gChannel.OnValueChanged += SetColor;
            _bChannel.OnValueChanged += SetColor;

            _nameField.onSubmit.AddListener(HandleNameSet);
            _setColorButton.onClick.AddListener(HandleColorSet);
        }

        private void OnDisable()
        {
            _rChannel.OnValueChanged -= SetColor;
            _gChannel.OnValueChanged -= SetColor;
            _bChannel.OnValueChanged -= SetColor;

            _nameField.onSubmit.RemoveListener(HandleNameSet);
            _setColorButton.onClick.RemoveListener(HandleColorSet);
        }

        public void Show(string currentName, Color currentColor)
        {
            SetupName(currentName);
            SetupColor(currentColor);

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void HandleNameSet(string name)
        {
            OnNameSet?.Invoke(name);
        }

        private void HandleColorSet()
        {
            OnColorSet?.Invoke(_color);
        }

        private void SetupName(string name)
        {
            _nameField.text = name;
        }

        private void SetupColor(Color color)
        {
            _rChannel.Value = color.r;
            _gChannel.Value = color.g;
            _bChannel.Value = color.b;

            SetColor();
        }

        private void SetColor()
        {
            _color.r = _rChannel.Value;
            _color.g = _gChannel.Value;
            _color.b = _bChannel.Value;
            _color.a = 1f;

            _colorView.color = _color;
        }
    }
}