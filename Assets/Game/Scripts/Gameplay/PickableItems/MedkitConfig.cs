using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "MedkitConfig",
        menuName = "Configs/Items/Medkit")]
    public class MedkitConfig : ItemConfig
    {
        public override ItemType Type => ItemType.Medkit;
        public int HealValue => _healValue;

        [SerializeField]
        private int _healValue;
    }
}