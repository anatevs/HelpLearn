using Gameplay;
using System;

namespace UI
{
    public class PlayerStatsController :
        IDisposable
    {
        private readonly HPComponent _hp;
        private readonly ScoreStorage _scoreStorage;

        private readonly PlayerStatsView _view;

        public PlayerStatsController(HPComponent hp,
            ScoreStorage scoreStorage,
            PlayerStatsView view)
        {
            _hp = hp;
            _scoreStorage = scoreStorage;
            _view = view;

            _hp.OnHPChanged += SetHP;
            _scoreStorage.OnValueChanged += SetScore;
        }

        void IDisposable.Dispose()
        {
            _hp.OnHPChanged -= SetHP;
            _scoreStorage.OnValueChanged -= SetScore;
        }

        private void SetHP(int hp)
        {
            _view.SetHP(hp.ToString());
        }

        private void SetScore(int score)
        {
            _view.SetScore(score.ToString());
        }
    }
}