using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "RegenHPModifierConfig",
        menuName = "Configs/Modifiers/RegenHPModifier")]
    public sealed class RegenHPModifierConfig : GameModifierConfig
    {
        public override string Name => $"{_name} by {_regenValue} every {_period} s";

        public int RegenValue => _regenValue;

        public float Period => _period;

        [SerializeField]
        private int _regenValue;

        [SerializeField]
        private float _period;
    }
}