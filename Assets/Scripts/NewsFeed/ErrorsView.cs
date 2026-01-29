using TMPro;
using UnityEngine;

namespace NewsFeed
{
    public class ErrorsView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        public void ShowText(string text)
        {
            _text.text = text;
        }
    }
}