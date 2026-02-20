using System;
using UnityEngine;

namespace Gameplay
{
    public class HPComponent : MonoBehaviour
    {
        public Action<int> OnDamaged;

        private int _hp;

        public void Init(int startHP)
        {
            _hp = startHP;
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            _hp = _hp < 0 ? 0 : _hp;

            OnDamaged?.Invoke(_hp);

            Debug.Log($"damage {damage} to {gameObject.name}, now hp is {_hp}");
        }
    }
}