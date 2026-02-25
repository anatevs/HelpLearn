using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerConfig", 
        menuName = "Configs/Player")]
    public sealed class GameConfig : ScriptableObject
    {
        public MovementInfo Movement => _movementInfo;
        public int HP => _hp;
        public int WinScore => _winScore;

        [SerializeField]
        private MovementInfo _movementInfo;

        [SerializeField]
        private int _hp;

        [SerializeField]
        private int _winScore = 10;
    }
}