using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(MeshRenderer))]
    public class WeaponBody : MonoBehaviour
    {
        [SerializeField]
        private Material _active;

        [SerializeField]
        private Material _inactive;

        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void ShowActive(bool isActive)
        {
            if (isActive)
            {
                _renderer.material = _active;
                return;
            }

            _renderer.material = _inactive;
        }
    }
}