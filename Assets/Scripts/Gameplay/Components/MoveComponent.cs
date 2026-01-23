using UnityEngine;

namespace Gameplay
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        public void MoveUpdate(Vector3 moveDirection)
        {
            if (!GameSingleton.Instance.GameManager.IsPaused)
            {
                var newPos = transform.position + moveDirection * _speed * Time.deltaTime;

                if (GameSingleton.Instance.GameBorders.IsInBorders(newPos))
                {
                    transform.position = newPos;
                }
            }
        }
    }
}