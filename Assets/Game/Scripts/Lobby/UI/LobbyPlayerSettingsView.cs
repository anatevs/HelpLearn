using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.UI
{
    public class LobbyPlayerSettingsView : MonoBehaviour
    {
        public event Action<string> OnNameSetRequested;
        public event Action<Color> OnColorSet;

        public event Action<bool> OnReadyChanged;

        private Color _color = Color.white;

        private int[] _nameLengthRange;

        [SerializeField]
        private TMP_Text _currentName;

        [SerializeField]
        private TMP_Text _nameRangeInfo;

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

        [SerializeField]
        private LobbyReadyView _readyView;

        private void OnEnable()
        {
            _rChannel.OnValueChanged += SetColor;
            _gChannel.OnValueChanged += SetColor;
            _bChannel.OnValueChanged += SetColor;

            _nameField.onSubmit.AddListener(HandleNameSetRequest);
            _setColorButton.onClick.AddListener(HandleColorSet);

            _readyView.OnReadyChanged += HandleReadyChange;
        }

        private void OnDisable()
        {
            _rChannel.OnValueChanged -= SetColor;
            _gChannel.OnValueChanged -= SetColor;
            _bChannel.OnValueChanged -= SetColor;

            _nameField.onSubmit.RemoveListener(HandleNameSetRequest);
            _setColorButton.onClick.RemoveListener(HandleColorSet);

            _readyView.OnReadyChanged -= HandleReadyChange;

            UnsubscribeNameColor();
        }

        private void UnsubscribeNameColor()
        {
            OnNameSetRequested = null;
            OnColorSet = null;
        }

        public void Show(string currentName, Color currentColor, int[] nameLengthRange)
        {
            SetNameTitle(currentName);

            SetupColor(currentColor);

            gameObject.SetActive(true);

            SetNameRangeInfoText(nameLengthRange);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetNameTitle(string name)
        {
            _currentName.text = name;
        }

        private void HandleNameSetRequest(string name)
        {
            if (name.Length < _nameLengthRange[0] || name.Length > _nameLengthRange[1])
            {
                return;
            }

            OnNameSetRequested?.Invoke(name);
        }

        private void HandleColorSet()
        {
            OnColorSet?.Invoke(_color);
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

        private void HandleReadyChange(bool isReady)
        {
            OnReadyChanged?.Invoke(isReady);
        }

        private void SetNameRangeInfoText(int[] range)
        {
            _nameLengthRange = range;
            _nameRangeInfo.text = $"(from {range[0]} to {range[1]} symbols,\nnot a default (NameIndex) for others or existing)";
        }
    }
}