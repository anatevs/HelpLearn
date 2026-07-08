using Gameplay;
using System;

namespace UI
{
    public class MatchTimerPresenter :
        IDisposable
    {
        private readonly MatchTimerView _view;

        private readonly MatchTimer _matchTimer;

        public MatchTimerPresenter(MatchTimerView view, MatchTimer matchTimer)
        {
            _view = view;
            _matchTimer = matchTimer;

            _matchTimer.OnRemainTimeChanged += SetTime;
        }

        public void Dispose()
        {
            _matchTimer.OnRemainTimeChanged -= SetTime;
        }

        private void SetTime(double timeSeconds)
        {
            var time = TimeSpan.FromSeconds(timeSeconds);

            _view.SetText($"{time:mm\\:ss}");
        }
    }
}