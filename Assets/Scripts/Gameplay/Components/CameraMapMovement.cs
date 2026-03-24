namespace Gameplay
{
    public class CameraMapMovement : CameraFollowMovement
    {
        private void LateUpdate()
        {
            var pos = GetFollowingPosition(_target);
            pos.y = transform.position.y;

            transform.position = pos;
        }
    }
}