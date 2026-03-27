using UnityEngine;

namespace Gameplay
{
    public class WeaponLook : MonoBehaviour
    {
        public Transform ShotPoint => _shotPoint;

        [SerializeField]
        private Transform _shotPoint;
    }
}