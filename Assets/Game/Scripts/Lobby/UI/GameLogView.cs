using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameLogView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _logText;

        [SerializeField]
        private RectTransform _textTransform;

        [SerializeField]
        private RectTransform _contentTransform;

        [SerializeField]
        private float _downTextSpace = 10f;

        private string _currentLogs = "";

        public void AddLog(string log)
        {
            SetCurrentLogs($"{_currentLogs}{log}\n");
        }

        public void ClearLogView()
        {
            SetCurrentLogs("");
        }

        private void SetCurrentLogs(string logs)
        {
            _currentLogs = logs;
            _logText.text = _currentLogs;
            LayoutRebuilder.ForceRebuildLayoutImmediate(_textTransform);

            _contentTransform.sizeDelta = new Vector2(
                _contentTransform.sizeDelta.x,
                _textTransform.rect.height + _downTextSpace);
        }
    }
}