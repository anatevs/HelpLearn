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
        private readonly int _startHP;
        private readonly int _maxHP;

        public SimpleHP(int startHP, int maxHP)
        {
            _hp = startHP;
            _startHP = startHP;
            _maxHP = maxHP;
        }

        public void ResetLevel()
        {
            ChangeHP(_startHP);
        }

        public void Heal(int addHP)
        {
            ChangeHP(_hp + addHP);
        }

        public void TakeDamage(int damage)
        {
            var newHP = _hp - damage;

            ChangeHP(newHP);

            if (_hp == 0)
            {
                OnKilled?.Invoke();
            }
        }

        private void ChangeHP(int newHP)
        {
            var oldHP = _hp;

            _hp = Mathf.Clamp(newHP, 0, _maxHP);

            if (oldHP != _hp)
            {
                OnHPChanged?.Invoke(_hp);
            }
        }
    }
}