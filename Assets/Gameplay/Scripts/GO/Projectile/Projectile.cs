using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        public Action<Projectile> OnCollided;

        private float _speed = 6f;

        private int _damage = 1;

        private MovementComponent _movement;

        private Vector3 _direction = Vector3.up;

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
        }

        private void Update()
        {
            _movement.MoveUpdate(_direction, _speed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<HPComponent>(out var hpComponent))
            {
                hpComponent.TakeDamage(_damage);
            }

            OnCollided?.Invoke(this);
        }

        public void SetParams(int damage, float speed, Vector3 direction)
        {
            _damage = damage;
            _speed = speed;
            _direction = direction;
        }
    }
}