using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MovementComponent))]
    public class Enemy : MonoBehaviour
    {
        public string Name => _config.Name;

        [SerializeField]
        private EnemyConfig _config;

        private MovementComponent _movement;

        private EnemyBehaviour _currentBehaviour;

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();

            _currentBehaviour = new PatrolBehaviour(this);
        }

        private void Update()
        {
            _currentBehaviour.ActUpdate();
        }
    }
}