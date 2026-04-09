using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class WaveInfoView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _waveNumber;

        [SerializeField]
        private TMP_Text _killedAmount;

        [SerializeField]
        private TMP_Text _score;

        public void SetupView(string number, string killed, string score)
        {
            _waveNumber.text = number;
            _killedAmount.text = killed;
            _score.text = score;
        }

        public void SetNumber(string number)
        {
            _waveNumber.text = number;
        }

        public void SetKilledAmount(string killed)
        {
            _killedAmount.text = killed;
        }
        public void SetScore(string score)
        {
            _score.text = score;
        }
    }
}