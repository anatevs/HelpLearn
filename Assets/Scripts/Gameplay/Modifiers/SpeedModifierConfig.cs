using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "SpeedModifierConfig",
        menuName = "Configs/Modifiers/SpeedModifier")]
    public class SpeedModifierConfig : GameModifierConfig
    {
        public override string Name => $"{_name} {_multiplier}";

        public float Multiplier => _multiplier;

        [SerializeField]
        private float _multiplier = 2f;
    }
}