using Assets.Input;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovingConfig _config;

        private InputHandler _input;

        private Rigidbody _rb;

        private float _castRadius = 0.4f;

        private bool _jumpStarted = false;

        private bool _jumpForced = false;

        private int _jumpCount = 0;

        private float _currentSpeed;

        private Vector3 _velocityChange = Vector3.zero;

        private Vector3 _castSphereCenterShift;

        private float _groundDrag;

        private float _groundCastDistance;

        private readonly float _velocityLerpCoef = 0.9f;

        public void Init(InputHandler input)
        {
            _input = input;
            _input.OnJupmed += Jump;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            _currentSpeed = _config.WalkSpeed;

            _groundDrag = _rb.linearDamping;
        }

        private void OnDisable()
        {
            if (_input != null)
            {
                _input.OnJupmed -= Jump;
            }
        }

        public void UpdateMovement()
        {
            _currentSpeed = _config.WalkSpeed;
        }

        public void FixedUpdateMovement()
        {
            UpdateVelocityChange(_currentSpeed, _input.Move);
            _velocityChange = Vector3.Lerp(_velocityChange, _currentSpeed * Time.fixedDeltaTime * _input.Move, _velocityLerpCoef);

            _rb.AddRelativeForce(_velocityChange, ForceMode.VelocityChange);

            if (_jumpForced && _rb.linearVelocity.y < 0 && !_jumpStarted)
            {
                _jumpForced = false;
            }

            if (_jumpStarted)
            {
                _jumpForced = true;

                _rb.isKinematic = false;
                _rb.AddForce(_config.JumpSpeed * Vector3.up, ForceMode.VelocityChange);
                _rb.linearDamping = 0;
            }

            if (!_jumpForced)
            {
                _groundCastDistance = 
                    Mathf.Abs(_rb.linearVelocity.y) * Time.fixedDeltaTime + _castSphereCenterShift.y;



                if (Physics.SphereCast(transform.position + _castSphereCenterShift, _castRadius,
                    Vector3.down, out RaycastHit downHit, _groundCastDistance, _config.GroundedLayers))
                {
                    HandleGroundHit(downHit);
                }
            }

            _jumpStarted = false;
        }

        private void UpdateVelocityChange(float speed, Vector3 moveDir)
        {
            _velocityChange = Vector3.Lerp(_velocityChange, speed * Time.fixedDeltaTime * moveDir, _velocityLerpCoef);
        }

        private void HandleGroundHit(RaycastHit hit)
        {
            _rb.linearDamping = _groundDrag;
            _jumpCount = 0;
        }

        private void Jump()
        {
            if (CanJump())
            {
                _jumpStarted = true;

                _jumpCount++;
            }
        }

        private bool CanJump()
        {
            return ((_jumpCount >= 0 && _jumpCount < _config.MaxJumps));
        }
    }
}