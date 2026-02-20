using UnityEngine;

namespace Gameplay
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        [SerializeField]
        private PatrolZones _patrolZones;

        private void Awake()
        {
            _patrolZones.Init();
        }

        private void Start()
        {
            EnemySpawnService.Instance.Init(_player, _patrolZones.Locations);

            EnemySpawnService.Instance.StartSpawn();
        }
    }
}