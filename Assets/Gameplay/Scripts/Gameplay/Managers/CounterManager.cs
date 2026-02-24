using EventBusNamespace;
using System;
using UI;

namespace Gameplay
{
    public class CounterManager<Tevent> :
        IDisposable
        where Tevent : GameEvent
    {
        private int _counter;

        private readonly CounterView _counterView;

        public CounterManager(int startCount, CounterView counterView)
        {
            _counter = startCount;
            _counterView = counterView;

            SetToView();

            EventBus.Subscribe<Tevent>(HandleEvent);
        }

        void IDisposable.Dispose()
        {
            EventBus.Unsubscribe<Tevent>(HandleEvent);
        }

        private void HandleEvent(Tevent e)
        {
            _counter++;

            SetToView();
        }

        private void SetToView()
        {
            _counterView.SetCountText(_counter.ToString());
        }
    }
}