using System;
using UnityEngine;

namespace Gameplay
{
    public class Target : MonoBehaviour
    {
        public event Action<Target> OnKilled;

        [SerializeField]
        private LayerMask _destroyLayers;

        private float _speed;

        private Vector3 _deltaMove;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void SetParameters(float speed, Vector3 moveDirection)
        {
            _speed = speed;

            _deltaMove = _speed * Time.fixedDeltaTime * moveDirection;
        }

        private void FixedUpdate()
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