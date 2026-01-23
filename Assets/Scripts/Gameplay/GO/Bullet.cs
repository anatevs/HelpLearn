using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MoveComponent))]
    public class Bullet : SpawnableGO
    {
        [SerializeField]
        private int _damage;

        private MoveComponent _characterMove;

        private void Awake()
        {
            _characterMove = GetComponent<MoveComponent>();
        }

        private void Update()
        {
            _characterMove.MoveUpdate(transform.forward);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<HPComponent>(out var hp))
            {
                hp.MakeDamage(_damage);
            }

            if (!other.isTrigger)
            {
                MakeUnspawn();
            }
        }
    }
}