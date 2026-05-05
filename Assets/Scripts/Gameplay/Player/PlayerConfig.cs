using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerConfig",
        menuName = "Configs/Player")]
    public sealed class PlayerConfig : ScriptableObject
    {
        public int StartHP => _startHP;

        public ItemConfig[] InitShowedItems => _initShowedItems;

        [SerializeField]
        private int _startHP;

        [SerializeField]
        private ItemConfig[] _initShowedItems;
    }
}