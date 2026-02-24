using TMPro;
using UnityEngine;

namespace UI
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _conterText;

        public void SetCountText(string countText)
        {
            _conterText.text = countText;
        }
    }
}