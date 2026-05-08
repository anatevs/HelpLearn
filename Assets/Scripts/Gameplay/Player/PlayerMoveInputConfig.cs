using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerInputConfig",
        menuName = "Configs/PlayerInput")]
    public class PlayerMoveInputConfig : ScriptableObject
    {
        public string TypeName => _typeName;
        public float Speed => _speed;
        public float RotationSpeed => _rotationSpeed;

        [SerializeField]
        private string _typeName;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _rotationSpeed;
    }
}