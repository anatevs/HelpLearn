using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SphereCollider))]
    public class SphereTriggerComponent : MonoBehaviour
    {
        public event Action<Transform> OnEntered;
        public event Action<Transform> OnExited;

        private SphereCollider _sphereCollider;

        public void Init()
        {
            _sphereCollider = GetComponent<SphereCollider>();

            _sphereCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            OnEntered?.Invoke(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExited?.Invoke(other.transform);
        }

        public void SetRadius(float radius)
        {
            _sphereCollider.radius = radius;
        }
    }
}