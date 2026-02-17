using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class AttackDetector : MonoBehaviour
    {
        public event Action OnPlayerDetected;

        private CircleCollider2D _circle;

        public void Init()
        {
            _circle = GetComponent<CircleCollider2D>();
        }

        public void SetDetectDistance(float distance)
        {
            _circle.radius = distance;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Player>(out _))
            {
                OnPlayerDetected?.Invoke();
            }
        }
    }
}