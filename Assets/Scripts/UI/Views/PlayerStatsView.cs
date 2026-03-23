using UnityEngine;

namespace UI
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField]
        private StatView _hpView;

        [SerializeField]
        private StatView _scoreView;

        public void SetHP(string hp)
        {
            _hpView.SetText(hp);
        }

        public void SetScore(string score)
        {
            _scoreView.SetText(score);
        }
    }
}