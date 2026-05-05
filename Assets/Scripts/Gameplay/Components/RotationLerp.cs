using UnityEngine;

namespace Gameplay
{
    public sealed class RotationLerp : IRotationService
    {
        public Transform Rotatable => _rotatable;

        private readonly Transform _rotatable;

        public RotationLerp(Transform rotatable)
        {
            _rotatable = rotatable;
        }

        public void RotateUpdate(Vector3 lookDirection, float rotationSpeed)
        {
            var newRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            _rotatable.rotation = Quaternion.Lerp(_rotatable.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }
}