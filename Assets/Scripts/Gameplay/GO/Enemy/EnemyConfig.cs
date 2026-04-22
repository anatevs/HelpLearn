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
        public float StopDistance => _stopDistance;
        public float FollowSpeed => _followSpeed;
        public int KillReward => _killReward;
        public Color MapIconColor => _mapIconColor;


        [Header("Common")]
        [SerializeField]
        private string _name;

        [SerializeField]
        private int _hp;

        [SerializeField]
        private float _rotationSpeed;

        [Header("Attacking")]
        [SerializeField]
        private float _followSpeed;

        [SerializeField]
        private float _stopDistance = 1;

        [Header("Killing")]
        [SerializeField]
        private int _killReward = 1;

        [Header("UI")]
        [SerializeField]
        private Color _mapIconColor;
    }
}