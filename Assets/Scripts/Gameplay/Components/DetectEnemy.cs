using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SphereCollider))]
    public class DetectEnemy : MonoBehaviour
    {
        public event Action<Enemy> OnNewEnemy;

        [SerializeField]
        private float _battleDistance;

        private SphereCollider _battleZone;

        private void Awake()
        {
            _battleZone = GetComponent<SphereCollider>();

            _battleZone.radius = _battleDistance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                if (!enemy.IsAttack)
                {
                    OnNewEnemy?.Invoke(enemy);
                }
            }
        }
    }
}