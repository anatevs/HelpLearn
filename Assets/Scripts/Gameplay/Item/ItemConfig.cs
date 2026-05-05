using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ItemConfig",
        menuName = "Configs/Item")]
    public sealed class ItemConfig : ScriptableObject
    {
        public string Name => _name;
        public int Amount => _amount;

        [SerializeField]
        private string _name;

        [SerializeField]
        private int _amount;
    }
}