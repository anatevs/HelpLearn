using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig",
        menuName = "Configs/PlayerMovement")]
    public class PlayerMovementConfig : ScriptableObject
    {
        public LayerMask GroundedLayers => _groundedLayers;
        public float WalkSpeed => _walkSpeed;
        public float RunSpeed => _runSpeed;
        public float RotationSpeed => _rotSpeed;
        public float[] XRotRange => _xRotRange;

        public float JupmForce => _jumpForce;
        public float JumpFallMultiplier => _jumpFallMultiplier;

        [SerializeField]
        private LayerMask _groundedLayers;

        [SerializeField]
        private float _walkSpeed = 1f;

        [SerializeField]
        private float _runSpeed = 2f;

        [SerializeField]
        private float _jumpForce = 10f;

        [SerializeField]
        private float _jumpFallMultiplier = 2f;

        [SerializeField]
        private float _rotSpeed = 20f;

        [SerializeField]
        private float[] _xRotRange = { -45f, 45f };
    }
}