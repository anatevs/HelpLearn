using UnityEngine;

namespace Gameplay
{
    public class ShootComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _shootPoint;

        public void Shoot()
        {
            if (!GameSingleton.Instance.GameManager.IsPaused)
            {
                GameSingleton.Instance.BulletManager.Shoot(_shootPoint);
            }
        }
    }
}