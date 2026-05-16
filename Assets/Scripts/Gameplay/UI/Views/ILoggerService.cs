using System;

namespace UI
{
    public interface ILoggerService :
        IDisposable
    {
        public LogMessagesConfig LogMessagesConfig { get; }

        public void Log(string message);

        public void LogHPChanged(int newHP);
        public void LogKilled();
        public void LogSpawnItem(string name);
        public void LogItemPicked(string name, int newValue);
        public void LogInputSwitched(string name);
        public void LogModifierApply(string name);
        public void LogModifierCancel(string name);
    }
}