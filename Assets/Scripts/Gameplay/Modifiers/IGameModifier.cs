using System;

namespace Gameplay
{
    public interface IGameModifier :
        IDisposable
    {
        public string Name { get; }

        public void OnEnterGameplay();

        public void OnExitGameplay();

        public void Tick(float deltaTime);
    }
}