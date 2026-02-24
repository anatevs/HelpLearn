using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ItemConfig",
        menuName = "Configs/Item")]
    public class ItemConfig : ScriptableObject
    {
        public string Name => _name;

        [SerializeField]
        private string _name;
    }
}