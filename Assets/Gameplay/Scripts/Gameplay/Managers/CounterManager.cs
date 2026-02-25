using EventBusNamespace;
using System;
using UI;

namespace Gameplay
{
    public class CounterManager<Tevent> :
        IDisposable
        where Tevent : GameEvent
    {
        protected int _countValue;

        private readonly CounterView _counterView;

        public CounterManager(int startCount, CounterView counterView)
        {
            _countValue = startCount;
            _counterView = counterView;

            SetToView();

            EventBus.Subscribe<Tevent>(HandleEvent);
        }

        void IDisposable.Dispose()
        {
            EventBus.Unsubscribe<Tevent>(HandleEvent);
        }

        public void Reset(int count)
        {
            _countValue = count;

            SetToView();
        }

        protected virtual void HandleEvent(Tevent e)
        {
            _countValue++;

            SetToView();
        }

        protected void SetToView()
        {
            _counterView.SetCountText(_countValue.ToString());
        }
    }
}