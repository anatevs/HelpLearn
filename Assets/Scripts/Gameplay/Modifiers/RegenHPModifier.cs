using System;

namespace Gameplay
{
    public sealed class RegenHPModifier : IGameModifier
    {
        public string Name => _config.Name;

        public Type ConfigType => _config.GetType();

        private readonly RegenHPModifierConfig _config;

        private readonly IHealth _health;

        private float _counter = 0;

        public RegenHPModifier(RegenHPModifierConfig config,
            IHealth health)
        {
            _config = config;
            _health = health;
        }

        public void OnEnterGameplay()
        {
            _counter = 0;
        }

        public void OnExitGameplay()
        {
            _counter = 0;
        }

        public void Tick(float deltaTime)
        {
            _counter += deltaTime;

            if (_counter >= _config.Period)
            {
                _counter = 0;
                _health.Heal(_config.RegenValue);
            }
        }
    }
}