using System;
using UnityEngine;

namespace Gameplay
{
    public class HPComponent : MonoBehaviour
    {
        public event Action OnDestroyed;

        [SerializeField]
        private int _startHP;

        private int _hp;

        private void Awake()
        {
            Reset();
        }

        public void MakeDamage(int damage)
        {
            _hp -= damage;
            _hp = Mathf.Clamp(_hp, 0, _hp);

            if (_hp == 0)
            {
                OnDestroyed?.Invoke();
            }
        }

        public void Reset()
        {
            _hp = _startHP;
        }
    }
}