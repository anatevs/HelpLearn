using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CharacterDataConfig",
        menuName = "Configs/CharacterData")]
    public sealed class CharacterDataConfig : ScriptableObject
    {
        public int StartHP => _startHP;

        public int StartScore => _startScore;

        [SerializeField]
        private int _startHP = 10;

        [SerializeField]
        private int _startScore = 0;
    }
}