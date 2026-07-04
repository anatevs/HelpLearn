using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LocalMessagesView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _messageText;

        [SerializeField]
        private float _showTime;

        private Coroutine _showCoroutine;

        private WaitForSeconds _showWaiting;

        private void Awake()
        {
            _showWaiting = new WaitForSeconds(_showTime);
            HideMessage();
        }

        public void ShowMessage(string message)
        {
            if (_showCoroutine != null)
            {
                StopCoroutine(_showCoroutine);
            }

            _showCoroutine = StartCoroutine(ShowMessageCoroutine(message));
        }

        private IEnumerator ShowMessageCoroutine(string message)
        {
            _messageText.text = message;
            _messageText.gameObject.SetActive(true);

            yield return _showWaiting;

            HideMessage();

            _showCoroutine = null;
        }

        private void HideMessage()
        {
            _messageText.text = "";
            _messageText.gameObject.SetActive(false);
        }
    }
}