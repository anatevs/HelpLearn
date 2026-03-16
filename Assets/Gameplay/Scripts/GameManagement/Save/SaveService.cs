using EventBusNamespace;
using System;

namespace GameManagement
{
    public class SaveService :
        IDisposable
    {
        private readonly EventBus _eventBus;

        private readonly HistorySaver _historySaver = new();

        public SaveService(EventBus eventBus)
        {
            _eventBus = eventBus;

            _historySaver.Init();

            _eventBus.OnGameEvent += _historySaver.WriteEvent;
        }

        void IDisposable.Dispose()
        {
            _eventBus.OnGameEvent -= _historySaver.WriteEvent;
        }
    }
}