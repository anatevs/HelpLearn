using UnityEngine;

namespace Gameplay
{
    public class RotationComponent : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed;

        public void RotateUpdate(Vector3 direction)
        {
            if (direction != Vector3.zero && !GameSingleton.Instance.GameManager.IsPaused)
            {
                var newRotation = Quaternion.LookRotation(direction, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }
}