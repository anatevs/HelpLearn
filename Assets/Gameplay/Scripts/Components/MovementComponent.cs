using UnityEngine;

namespace Gameplay
{
    public class MovementComponent : MonoBehaviour
    {
        public void MoveUpdate(Vector3 direction, float speed)
        {
            var newPos = Vector3.Lerp(transform.position, transform.position + direction, speed * Time.deltaTime);

            transform.position = newPos;
        }
    }
}