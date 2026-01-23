using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class InputManager : MonoBehaviour
    {
        public event Action OnShot;

        public Vector3 Move => _move;

        public Vector3 MousePoint => _mousePoint;

        [SerializeField]
        private LayerMask _mouseRayColliders;

        [SerializeField]
        private float _mouseRayDistance = 400f;

        private Vector3 _move;

        private Vector3 _mousePoint;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Update()
        {
            var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");

            _move = new Vector3(hor, 0, ver);
            _move.Normalize();

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                OnShot?.Invoke();
            }

            var screenPointRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(screenPointRay, out var hitInfo, _mouseRayDistance, _mouseRayColliders))
            {
                _mousePoint = hitInfo.point;
            }
        }
    }
}