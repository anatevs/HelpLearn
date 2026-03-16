using EventBusNamespace;
using System;

namespace GameManagement
{
    public class SaveService :
        IDisposable
    {
        private EventBus _eventBus;

        private HistorySaver _historySaver;

        public SaveService(EventBus eventBus)
        {
            _eventBus = eventBus;

            _historySaver = new HistorySaver(_eventBus);

            _historySaver.Init();

            _historySaver.OnEnable();
        }

        void IDisposable.Dispose()
        {
            _historySaver.OnDisable();
        }
    }
}