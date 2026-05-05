using UnityEngine;

namespace Gameplay
{
    public class RotationComponent : MonoBehaviour
    {
        public void Rotate(Vector3 direction, float rotationSpeed, float deltaTime)
        {
            var targetRotation = Quaternion.FromToRotation(Vector3.forward, direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * deltaTime);
        }
    }
}