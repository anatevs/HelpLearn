using EventBusNamespace;

namespace Gameplay
{
    public class ScoreStorage : CounterStorage
    {
        private readonly int _winScore;

        private readonly EventBus _eventBus;

        public ScoreStorage(int startCount,
            int winScore,
            EventBus eventBus) : base(startCount)
        {
            _winScore = winScore;
            _eventBus = eventBus;
        }

        public override void ChangeValue(int deltaValue)
        {
            base.ChangeValue(deltaValue);

            if (_countValue >= _winScore)
            {
                _eventBus.RaiseEvent(new GameWinEvent());
            }
        }
    }
}