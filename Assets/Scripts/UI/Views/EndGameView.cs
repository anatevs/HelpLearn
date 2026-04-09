using TMPro;
using UnityEngine;

namespace UI
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _loseText;

        [SerializeField]
        private TMP_Text _winText;

        [SerializeField]
        private WavesPanelView _wavesPanel;

        public void Show(bool isLose)
        {
            gameObject.SetActive(true);
            _loseText.gameObject.SetActive(isLose);
            _winText.gameObject.SetActive(!isLose);

            _wavesPanel.Show(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}