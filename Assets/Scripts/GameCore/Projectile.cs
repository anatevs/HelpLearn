using System;
using System.Collections;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public sealed class Projectile : MonoBehaviour
    {
        public event Action<Projectile> Destroyed;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        [SerializeField]
        private float _lifetime = 5f;

        [SerializeField]
        private LayerMask _killableLayers;

        private float _speed = 5f;

        private Rigidbody _rigidbody;

        private Coroutine _autoDestroy = null;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(transform.forward * _speed);

            _autoDestroy = StartCoroutine(AutoDestroy());
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((_killableLayers.value & (1 << other.gameObject.layer)) != 0)
            {
                Destroy(other.gameObject);
                OnDestoryed();

                Debug.Log("Target hit!");
            }
        }

        public void OnDestoryed()
        {
            _rigidbody.isKinematic = true;
            Destroyed?.Invoke(this);

            if (_autoDestroy != null)
            {
                StopCoroutine(_autoDestroy);
            }
        }

        private IEnumerator AutoDestroy()
        {
            yield return new WaitForSeconds(_lifetime);
            OnDestoryed();
        }
    }
}