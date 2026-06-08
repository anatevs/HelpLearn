using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.UI
{
    public class LobbySlotView : MonoBehaviour
    {
        public event Action OnRemoveClicked;

        [SerializeField]
        private Image _colorView;

        [SerializeField]
        private TMP_Text _name;

        [SerializeField]
        private TMP_Text _status;

        [SerializeField]
        private Button _removeButton;

        private string _emptyName;
        private Color _emptyColor;
        private readonly string _emptyStatus = "";

        private void Awake()
        {
            _emptyName = _name.text;
            _emptyColor = _colorView.color;
        }

        private void OnEnable()
        {
            _removeButton.onClick.AddListener(HandleRemoveClick);
        }

        private void OnDisable()
        {
            _removeButton.onClick.RemoveListener(HandleRemoveClick);
        }

        public void SetEmpty()
        {
            SetName(_emptyName);
            SetColor(_emptyColor);
            SetStaus(_emptyStatus);
        }

        public void SetColor(Color color)
        {
            _colorView.color = color;
        }

        public void SetName(string name)
        {
            _name.text = name;
        }

        public void SetStaus(string status)
        {
            _status.text = status;
        }

        public void ShowRemoveButton(bool isShow)
        {
            _removeButton.gameObject.SetActive(isShow);
        }

        private void HandleRemoveClick()
        {
            OnRemoveClicked?.Invoke();
        }
    }
}