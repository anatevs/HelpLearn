using EventBusNamespace;
using System;
using UI;

namespace Gameplay
{
    public class CounterController<Tevent> :
        IDisposable
        where Tevent : GameEvent
    {
        protected CounterStorage _storage;

        private readonly CounterView _counterView;

        private readonly EventBus _eventBus;

        public CounterController(CounterStorage storage,
            CounterView counterView,
            EventBus eventBus)
        {
            _storage = storage;
            _counterView = counterView;

            SetToView();

            _eventBus = eventBus;
            _eventBus.Subscribe<Tevent>(HandleEvent);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<Tevent>(HandleEvent);
        }

        public void Reset()
        {
            _storage.ResetValue();

            SetToView();
        }

        protected virtual void HandleEvent(Tevent e)
        {
            _storage.ChangeValue(1);

            SetToView();
        }

        protected void SetToView()
        {
            _counterView.SetCountText(_storage.Value.ToString());
        }
    }
}