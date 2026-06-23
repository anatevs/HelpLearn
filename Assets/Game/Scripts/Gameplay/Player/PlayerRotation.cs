using Assets.Input;
using UnityEngine;

namespace Gameplay
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovingConfig _config;

        private InputHandler _input;

        private Rigidbody _rb;

        private float _xRotation;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Init(InputHandler input)
        {
            _input = input;
        }

        public void FixedUpdateRotation()
        {
            _rb.AddRelativeTorque(
                _input.LookAngle.x * _config.RotationSpeed * Time.fixedDeltaTime * Vector3.up,
                ForceMode.Force);
        }
    }
}