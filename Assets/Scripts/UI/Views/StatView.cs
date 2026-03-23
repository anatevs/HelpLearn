using TMPro;
using UnityEngine;

namespace UI
{
    public class StatView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        public void SetText(string hpText)
        {
            _text.text = hpText;
        }
    }
}