using UnityEngine;

namespace Gameplay
{
    public class RotationComponent : MonoBehaviour
    {
        public void Rotate(Vector3 direction, float rotationSpeed, float deltaTime)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
            //transform.Rotate(Vector3.up, _input.LookAngle.x * rotationSpeed * deltaTime);
        }
    }
}