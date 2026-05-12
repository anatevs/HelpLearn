using System;

namespace GameManagement
{
    public interface IGameExit
    {
        public void AddDisposable(IDisposable disposable);

        public void QuitGame();
    }
}