using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    public sealed class Projectile : MonoBehaviour
    {
        public Action<Projectile> OnCollided;

        private float _speed = 6f;

        private int _damage = 1;

        private MovementComponent _movement;

        private Vector3 _direction = Vector3.forward;

        private float _damageRadius = 0.5f;

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
            _damageRadius = transform.localScale.y / 2;
        }

        private void Update()
        {
            _movement.MoveUpdate(_direction, _speed);


            if (Physics.SphereCast(transform.position, _damageRadius, _direction, out var hit, _speed * Time.deltaTime))
            {
                HandleCollision(hit.collider);
            }
        }

        private void HandleCollision(Collider collider)
        {
            if (collider.gameObject.TryGetComponent<HPComponent>(out var hpComponent))
            {
                hpComponent.TakeDamage(_damage);
            }

            OnCollided?.Invoke(this);
        }

        public void SetParams(int damage, float speed, Vector3 direction, float damageRadius)
        {
            _damage = damage;
            _speed = speed;
            _direction = direction;
            _damageRadius = damageRadius;
        }

        public void SetParams(ProjectileConfig config, Vector3 direction)
        {
            SetParams(config.Damage, config.Speed,
                direction, config.DamageRadius);
        }
    }
}