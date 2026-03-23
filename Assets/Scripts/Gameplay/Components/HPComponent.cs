using System;
using UnityEngine;

namespace Gameplay
{
    public class HPComponent : MonoBehaviour
    {
        public event Action<int> OnHPChanged;

        private int _hp = 0;

        public void TakeDamage(int damage)
        {
            var hp = _hp - damage;

            hp = Mathf.Clamp(hp, 0, hp);

            Init(hp);
        }

        public void Init(int hp)
        {
            _hp = hp;

            OnHPChanged?.Invoke(_hp);
        }
    }
}