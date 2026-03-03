using EventBusNamespace;
using UI;
using GameManagement;

namespace Gameplay
{
    public sealed class ScoreManager : CounterManager<ScoreChangedEvent>
    {
        private int _winScore;

        public ScoreManager(int startCount, CounterView counterView, int winScore) : base(startCount, counterView)
        {
            _winScore = winScore;
        }

        protected override void HandleEvent(ScoreChangedEvent e)
        {
            _countValue += e.Value;

            SetToView();

            if (_countValue >= _winScore)
            {
                EventBus.RaiseEvent(new GameWinEvent());
            }
        }
    }
}