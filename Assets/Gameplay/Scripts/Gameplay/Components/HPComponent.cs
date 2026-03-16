using System;
using UnityEngine;

namespace Gameplay
{
    public sealed class HPComponent : MonoBehaviour
    {
        public event Action<int> OnDamaged;

        public int HP => _hp;

        private int _hp;

        public void Init(int startHP)
        {
            _hp = startHP;
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            _hp = _hp < 0 ? 0 : _hp;

            OnDamaged?.Invoke(damage);
        }
    }
}