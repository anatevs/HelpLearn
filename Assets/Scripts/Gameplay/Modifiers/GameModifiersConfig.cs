using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ModifiersConfig",
        menuName = "Configs/Modifiers/Modifiers")]
    public sealed class GameModifiersConfig : ScriptableObject
    {
        public GameModifierConfig[] Configs => _configs;

        [SerializeField]
        private GameModifierConfig[] _configs;
    }
}