using UnityEngine;

namespace Gameplay
{
    public interface IRotationService
    {
        public Transform Rotatable { get; }

        public void RotateUpdate(Vector3 lookDirection, float rotationSpeed);
    }
}