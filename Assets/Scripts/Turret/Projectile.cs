using System;
using UnityEngine;

namespace Gameplay
{
    public class Projectile : MonoBehaviour
    {
        public event Action<Projectile> OnCollided;

        private float _speed;

        private LayerMask _damagableMask;

        public void SetParameters(float speed, LayerMask damagableMask)
        {
            _speed = speed;
            _damagableMask = damagableMask;
        }

        private void Update()
        {
            var deltaMove = _speed * Time.deltaTime * transform.forward;

            transform.Translate(deltaMove);
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((_damagableMask & (1 << other.gameObject.layer)) != 0
                && other.gameObject.TryGetComponent<Target>(out var target))
            {
                target.Kill();
            }

            OnCollided?.Invoke(this);
        }
    }
}