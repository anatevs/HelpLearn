using System;
using UnityEngine;

namespace Gameplay
{
    public class Target : MonoBehaviour,
        IPoolable,
        IMovableFixedUpd
    {
        public event Action<Target> OnKilled;

        [SerializeField]
        private LayerMask _destroyLayers;

        [Header("Visual")]
        [SerializeField]
        private Renderer[] _visuals;

        [SerializeField]
        private Animator _animator;

        private float _speed;

        private Vector3 _deltaMove;

        private Rigidbody _rb;

        private Collider _collider;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        public void Activate(bool isActive)
        {
            _collider.enabled = isActive;

            foreach (var renderer in _visuals)
            {
                renderer.enabled = isActive;
            }

            _animator.enabled = isActive;

            enabled = isActive;
        }

        public void SetParameters(float speed, Vector3 moveDirection)
        {
            _speed = speed;

            _deltaMove = _speed * Time.fixedDeltaTime * moveDirection;
        }

        public void MoveFixedUpd()
        {
            var nextPos = transform.position + _deltaMove;

            _rb.MovePosition(nextPos);
        }

        private void OnDisable()
        {
            OnKilled = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((_destroyLayers & (1 << other.gameObject.layer)) != 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            OnKilled?.Invoke(this);
        }
    }
}