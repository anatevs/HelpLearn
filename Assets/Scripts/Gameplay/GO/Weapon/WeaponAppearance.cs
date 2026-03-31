using UnityEngine;

namespace Gameplay
{
    public class WeaponAppearance : MonoBehaviour
    {
        public Transform ShotPoint => _shotPoint;

        [SerializeField]
        private Transform _shotPoint;
    }
}