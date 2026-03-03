using UnityEngine;

namespace UI
{
    public sealed class EndGameView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loseView;

        [SerializeField]
        private GameObject _winView;

        public void ShowWinLose(bool isWin)
        {
            _loseView.SetActive(!isWin);
            _winView.SetActive(isWin);

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}