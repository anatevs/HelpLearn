using GameManagement;
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
            GameStateService.Instance.CurrentState = GameState.Playing;

            EnemySpawnService.Instance.Init(_player, _patrolZones.Locations);

            ItemsService.Instance.Init();
        }
    }
}