using UnityEngine;

namespace Gameplay
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private Transform[] _leftRightBorders;

        [SerializeField]
        private Transform[] _bottomTopBorders;

        private void LateUpdate()
        {
            Move(_target);
        }

        private void Move(Transform followPoint)
        {
            var targetPoint = followPoint.position;

            var xPos = Mathf.Clamp(targetPoint.x, _leftRightBorders[0].position.x, _leftRightBorders[1].position.x);
            var zPos = Mathf.Clamp(targetPoint.z, _bottomTopBorders[0].position.z, _bottomTopBorders[1].position.z);

            targetPoint.x = xPos;
            targetPoint.z = zPos;

            transform.SetPositionAndRotation
                (targetPoint, followPoint.rotation);
        }
    }
}