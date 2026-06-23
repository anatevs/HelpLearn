using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerMovingConfig",
        menuName = "Configs/PlayerMoving")]
    public class PlayerMovingConfig : ScriptableObject
    {
        public LayerMask GroundedLayers => _groundedLayers;
        public float WalkSpeed => _walkSpeed;
        public float RotationSpeed => _rotSpeed;
        public float[] XRotRange => _xRotRange;

        public float JumpSpeed => _jumpSpeed;
        public int MaxJumps => _maxJumps;

        [SerializeField]
        private LayerMask _groundedLayers;

        [SerializeField]
        private float _walkSpeed = 1f;

        [SerializeField]
        private float _rotSpeed = 20f;

        [SerializeField]
        private float[] _xRotRange = { -45f, 45f };

        [SerializeField]
        private float _jumpSpeed = 5f;

        [SerializeField]
        private int _maxJumps = 1;


    }
}