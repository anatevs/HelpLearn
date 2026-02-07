using UnityEngine;

namespace Gameplay
{
    public class RotationZComponent : MonoBehaviour
    {
        public void RotateUpdate(Vector3 towardPoint, float speed)
        {
            var direction = towardPoint - transform.position;
            direction.z = 0;

            var newRotation = Quaternion.LookRotation(Vector3.forward, direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speed * Time.deltaTime);
        }
    }
}