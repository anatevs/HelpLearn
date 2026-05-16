using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "FastSpawnModifierConfig",
        menuName = "Configs/Modifiers/FastSpawnModifier")]
    public sealed class FastSpawnModifierConfig : GameModifierConfig
    {
        public override string Name => $"{_name} by {_periodDivider} times";

        public float PeriodDivider => _periodDivider;

        [SerializeField]
        private float _periodDivider;
    }
}