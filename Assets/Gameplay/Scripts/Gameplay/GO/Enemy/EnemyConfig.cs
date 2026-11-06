using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemyConfig",
        menuName = "Configs/Enemy")]
    public sealed class EnemyConfig : ScriptableObject, IPrefabConfig<Enemy>
    {
        public Enemy Prefab
        {
            get => _prefab;
            set { _prefab = value; }
        }

        public string Name => _name;
        public AssetReference PrefabReference => _prefabReference;
        public int HP => _hp;
        public float RotationSpeed => _rotationSpeed;
        public EnemyStrategyType StartStrategy => _startStrategy;
        public float PatrolSpeed => _patrolSpeed;
        public float DetectDistance => _detectDistance;
        public float SqrNearDistance => _sqrNearDistance;
        public float FollowSpeed => _followSpeed;
        public float ShotPeriod => _shotPeriod;
        public int KillReward => _killReward;

        [Header("Common")]
        [SerializeField]
        private string _name;

        [SerializeField]
        private AssetReference _prefabReference;

        [SerializeField]
        private int _hp;

        [SerializeField]
        private float _rotationSpeed;

        [SerializeField]
        private EnemyStrategyType _startStrategy;

        [Header("Patrol")]
        [SerializeField]
        private float _patrolSpeed;

        [Header("Attacking")]
        [SerializeField]
        private float _followSpeed;

        [SerializeField]
        private float _detectDistance;

        [SerializeField]
        private float _sqrNearDistance = 1;

        [SerializeField]
        private float _shotPeriod;

        [SerializeField]
        private int _killReward = 1;

        private Enemy _prefab;
    }
}