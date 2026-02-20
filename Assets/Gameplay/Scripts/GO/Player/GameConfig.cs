using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerConfig", 
        menuName = "Configs/Player")]
    public class GameConfig : ScriptableObject
    {
        public MovementInfo Movement => _movementInfo;
        public int HP => _hp;

        [SerializeField]
        private MovementInfo _movementInfo;

        [SerializeField]
        private int _hp;
    }
}