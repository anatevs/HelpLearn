using TMPro;
using UnityEngine;

namespace UI
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _loseText;

        public void Show(bool isLose)
        {
            gameObject.SetActive(true);
            _loseText.gameObject.SetActive(isLose);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}