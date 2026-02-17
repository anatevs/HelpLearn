using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemyConfig",
        menuName = "Configs/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        public string Name => _name;
        public int HP => _hp;
        public float PatrolSpeed => _patrolSpeed;
        public float DetectSqrDistance => _detectSqrDistance;
        public float FollowSpeed => _followSpeed;
        public float RotationSpeed => _rotationSpeed;

        private float _detectSqrDistance;

        [Header("Common")]
        [SerializeField]
        private string _name;

        [SerializeField]
        private int _hp;

        [SerializeField]
        private float _rotationSpeed;

        [Header("Patrol")]
        [SerializeField]
        private float _patrolSpeed;

        [Header("Following")]
        [SerializeField]
        private float _followSpeed;

        [SerializeField]
        private float _detectDistance;

        public void Init()
        {
            _detectSqrDistance = _detectDistance * _detectDistance;
        }
    }
}