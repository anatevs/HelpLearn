using System;

namespace Gameplay
{
    public interface IGameModifier
    {
        public string Name { get; }

        public Type ConfigType { get; }

        public void OnEnterGameplay();

        public void OnExitGameplay();

        public void Tick(float deltaTime);
    }
}