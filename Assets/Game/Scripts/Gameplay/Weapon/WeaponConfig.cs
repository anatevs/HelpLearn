using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "WeaponConfig",
        menuName = "Configs/Weapon")]
    public class WeaponConfig : ScriptableObject
    {
        public string Name => _name;
        public int Damage => _damage;
        public float MaxDistance => _maxDistance;
        public float FireRate => _fireRate;
        public int Charge => _charge;
        public int ShootCost => _shootCost;
        public float RechargeTime => _rechargeTime;
        public WeaponTracer TracerPrefab => _tracerPrefab;

        [SerializeField]
        private string _name;

        [SerializeField]
        private int _damage;

        [SerializeField]
        private float _maxDistance;

        [SerializeField]
        private float _fireRate;

        [SerializeField]
        private int _charge;

        [SerializeField]
        private int _shootCost;

        [SerializeField]
        private float _rechargeTime;

        [SerializeField]
        private WeaponTracer _tracerPrefab;
    }
}