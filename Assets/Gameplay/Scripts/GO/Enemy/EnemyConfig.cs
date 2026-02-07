using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemyConfig",
        menuName = "Configs/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        public string Name => _name;

        public MovementInfo Movement => _movementInfo;

        [SerializeField]
        private string _name;

        [SerializeField]
        private MovementInfo _movementInfo;
    }
}