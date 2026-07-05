using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "GrenadeConfig",
        menuName = "Configs/Items/Grenade")]
    public class GrenadeConfig : ItemConfig
    {
        public override ItemType Type => ItemType.Grenade;

        public Grenade GrenadePrefab => _grenadePrefab;
        public float ThrowSpeed => _throwSpeed;
        public float GravityMultiplier => _gravityMultiplier;
        public WaitForSeconds ExplosionWait => new WaitForSeconds(_explosionDelay);
        public int Damage => _damage;
        public float DamageRadius => _damageRadius;
        public LayerMask DamageLayers => _damageLayers;

        [SerializeField]
        private Grenade _grenadePrefab;

        [SerializeField]
        private float _throwSpeed;

        [SerializeField]
        private float _gravityMultiplier;

        [SerializeField]
        private float _explosionDelay;

        [SerializeField]
        private int _damage;

        [SerializeField]
        private float _damageRadius;

        [SerializeField]
        private LayerMask _damageLayers;
    }
}