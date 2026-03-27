namespace Gameplay
{
    public class CameraMapMovement : CameraFollowMovement
    {
        private void LateUpdate()
        {
            var pos = GetFollowingPosition();
            pos.y = transform.position.y;

            transform.position = pos;
        }
    }
}