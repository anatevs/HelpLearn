namespace Gameplay
{
    public class CameraMainMovement : CameraFollowMovement
    {
        private void LateUpdate()
        {
            MoveAndRotate();
        }

        private void MoveAndRotate()
        {
            var targetPoint = GetFollowingPosition();

            transform.SetPositionAndRotation
                (targetPoint, _target.rotation);
        }
    }
}