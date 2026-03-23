using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "TouchDamageConfig",
        menuName = "Configs/TouchDamage")]
    public sealed class TouchDamageConfig : ScriptableObject
    {
        public float DamageRadius => _damageRadius;
        public int Damage => _damage;
        public float DamageCooldown => _damageCooldown;
        public Color DamageActiveColor => _damageActiveColor;
        public Color DamageInactiveColor => _damageInactiveColor;

        [SerializeField]
        private float _damageRadius;

        [SerializeField]
        private int _damage = 1;

        [SerializeField]
        private float _damageCooldown = 0.5f;

        [SerializeField]
        private Color _damageActiveColor;

        [SerializeField]
        private Color _damageInactiveColor;
    }
}