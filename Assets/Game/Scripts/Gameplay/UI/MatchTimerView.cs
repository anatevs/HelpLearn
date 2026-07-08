using TMPro;
using UnityEngine;

namespace UI
{
    public class MatchTimerView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _timeText;

        public void SetText(string text)
        {
            _timeText.text = text;
        }
    }
}