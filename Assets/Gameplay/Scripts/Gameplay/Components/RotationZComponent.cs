using UnityEngine;

namespace Gameplay
{
    public sealed class RotationZComponent : MonoBehaviour
    {
        public void RotateUpdate(Vector3 direction, float speed)
        {
            direction.z = 0;

            var newRotation = Quaternion.LookRotation(Vector3.forward, direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speed * Time.deltaTime);
        }
    }
}