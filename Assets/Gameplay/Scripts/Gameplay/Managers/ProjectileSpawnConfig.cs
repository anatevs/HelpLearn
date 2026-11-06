using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ProjectileSpawnConfig",
        menuName = "Configs/ProjectileSpawn")]
    public sealed class ProjectileSpawnConfig : ScriptableObject, IPrefabConfig<Projectile>
    {
        public Projectile Prefab
        {
            get => _prefab;
            set { _prefab = value; }
        }

        public string Name => "Projectile";

        public AssetReference PrefabReference => _prefabReference;
        public float Speed => _speed;
        public int Damage => _damage;
        public int PoolInitCount => _poolInitCount;

        private Projectile _prefab;

        [SerializeField]
        private AssetReference _prefabReference;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private int _damage;

        [SerializeField]
        private int _poolInitCount = 10;
    }
}