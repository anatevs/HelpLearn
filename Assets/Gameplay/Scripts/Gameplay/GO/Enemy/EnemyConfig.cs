using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemyConfig",
        menuName = "Configs/Enemy")]
    public sealed class EnemyConfig : ScriptableObject
    {
        public string Name => _name;
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
    }
}