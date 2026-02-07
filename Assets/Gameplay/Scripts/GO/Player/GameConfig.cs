using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerConfig", 
        menuName = "Configs/Player")]
    public class GameConfig : ScriptableObject
    {
        public MovementInfo Movement => _movementInfo;

        [SerializeField]
        private MovementInfo _movementInfo;
    }
}