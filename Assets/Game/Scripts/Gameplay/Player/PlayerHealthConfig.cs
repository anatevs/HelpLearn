using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "HPConfig",
        menuName = "Configs/Health")]
    public class PlayerHealthConfig : ScriptableObject
    {
        public int InitHP => _initHP;

        public int MaxHP => _maxHP;

        public float RespawnDelay => _respawnDelay;

        [SerializeField]
        private int _initHP;

        [SerializeField]
        private int _maxHP;

        [SerializeField]
        private float _respawnDelay;
    }
}