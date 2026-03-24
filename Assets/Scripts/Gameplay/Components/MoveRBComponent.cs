using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveRBComponent : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _groundLayer;

        private Rigidbody _rb;

        private PlayerMovementConfig _config;

        private bool _isJumping = false;

        private float _groundCastLength = 1f;

        private Vector3 _currentVelocity = Vector3.zero;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Construct(PlayerMovementConfig config)
        {
            _config = config;
        }

        public void MoveFixedUpd(Vector3 walkDirection)
        {
            _rb.AddRelativeForce(_config.WalkSpeed * walkDirection, ForceMode.VelocityChange);

            if (_isJumping && _rb.linearVelocity.y < 0)
            {
                _rb.AddRelativeForce(Vector3.up * Physics.gravity.y * _config.JumpFallMultiplier, ForceMode.Acceleration);

                if (Physics.Raycast(transform.position, Vector3.down, _groundCastLength, _groundLayer))
                {
                    _isJumping = false;
                }
            }
        }

        public void Jump()
        {
            if (!_isJumping)
            {
                _rb.AddRelativeForce(_config.JupmForce * Vector3.up, ForceMode.Impulse);

                _isJumping = true;
            }
        }

        public void Init()
        {
            _currentVelocity = Vector3.zero;

            _rb.isKinematic = false;
            _rb.linearVelocity = _currentVelocity;
        }

        public void Pause()
        {
            _currentVelocity = _rb.linearVelocity;
            _rb.isKinematic = true;
        }

        public void Resume()
        {
            _rb.isKinematic = false;
            _rb.linearVelocity = _currentVelocity;
        }
    }
}