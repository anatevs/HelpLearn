using System;
using UnityEngine;

namespace Gameplay
{
    public sealed class SimpleHP : IHealth
    {
        public int HP => _hp;

        public event Action<int> OnHPChanged;
        public event Action OnKilled;

        private int _hp;

        public SimpleHP(int startHP)
        {
            _hp = startHP;
        }

        public void Heal(int addHP)
        {
            ChangeHP(_hp + addHP);
        }

        public void TakeDamage(int damage)
        {
            var newHP = _hp - damage;

            newHP = Mathf.Clamp(newHP, 0, newHP);

            ChangeHP(newHP);

            if (newHP == 0)
            {
                OnKilled?.Invoke();
            }
        }

        private void ChangeHP(int newHP)
        {
            _hp = newHP;
            OnHPChanged?.Invoke(newHP);
        }
    }
}