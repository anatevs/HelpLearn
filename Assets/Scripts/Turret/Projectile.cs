using System;
using UnityEngine;

namespace Gameplay
{
    public class Projectile : MonoBehaviour,
        IMovableUpd,
        ILifetimed
    {
        public ProjectileType ProjectileType => _type;

        public event Action<Projectile> OnDestroyed;

        private float _speed;

        private float _lifetime;

        private LayerMask _damagableMask;

        private float _timer = 0f;

        private ProjectileType _type;

        public void SetParameters(float speed, float lifetime, LayerMask damagableMask, ProjectileType type)
        {
            _speed = speed;
            _lifetime = lifetime;
            _damagableMask = damagableMask;
            _type = type;

            _timer = 0f;
        }

        public void MoveUpdate()
        {
            var deltaMove = _speed * Time.deltaTime * transform.forward;

            transform.Translate(deltaMove);
        }

        public void CheckLifetime(float deltaTime)
        {
            _timer += Time.deltaTime;

            if (_timer >= _lifetime)
            {
                _timer = 0f;

                OnDestroyed?.Invoke(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((_damagableMask & (1 << other.gameObject.layer)) != 0
                && other.gameObject.TryGetComponent<Target>(out var target))
            {
                target.Kill();
            }

            OnDestroyed?.Invoke(this);
        }
    }
}