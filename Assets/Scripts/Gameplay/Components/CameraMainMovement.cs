using UnityEngine;

namespace Gameplay
{
    public class CameraMainMovement : CameraFollowMovement
    {
        private void LateUpdate()
        {
            MoveAndRotate(_target);
        }

        private void MoveAndRotate(Transform followPoint)
        {
            var targetPoint = GetFollowingPosition(followPoint);

            transform.SetPositionAndRotation
                (targetPoint, followPoint.rotation);
        }
    }
}