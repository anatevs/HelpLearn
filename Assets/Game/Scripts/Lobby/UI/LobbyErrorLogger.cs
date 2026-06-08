using TMPro;
using UnityEngine;

namespace Network.UI
{
    public class LobbyErrorLogger : MonoBehaviour,
        ILobbyErrorLogger
    {
        [SerializeField]
        private TMP_Text _errorText;

        public void Log(string message)
        {
            _errorText.text = message;
        }
    }
}