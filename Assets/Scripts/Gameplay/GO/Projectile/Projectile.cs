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

        private float _castDistance = 1f;

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
        }

        private void Update()
        {
            _movement.MoveUpdate(_direction, _speed);

            if (Physics.Raycast(transform.position, _direction, out var hit, _castDistance))
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

        public void SetParams(int damage, float speed, Vector3 direction, float castDistance)
        {
            _damage = damage;
            _speed = speed;
            _direction = direction;
            _castDistance = castDistance;
        }
    }
}