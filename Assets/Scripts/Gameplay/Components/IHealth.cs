using System;

namespace Gameplay
{
    public interface IHealth
    {
        public event Action<int> OnHPChanged;
        public event Action OnKilled;

        public int HP { get; }

        public void TakeDamage(int damage);
        public void Heal(int addHP);
    }
}