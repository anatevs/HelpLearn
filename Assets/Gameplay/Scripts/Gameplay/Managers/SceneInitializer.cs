using EventBusNamespace;
using GameManagement;
using UnityEngine;

namespace Gameplay
{
    public sealed class SceneInitializer : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        [SerializeField]
        private PatrolZones _patrolZones;

        private void Awake()
        {
            _patrolZones.Init();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<RestartEvent>(ResetLevel);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<RestartEvent>(ResetLevel);
        }

        private void Start()
        {
            EnemySpawnService.Instance.Init(_player, _patrolZones.Locations);

            ProjectileSpawnService.Instance.Init();

            ItemsService.Instance.Init();

            PlayerCountersController.Instance.Init(_player);

            EventBus.RaiseEvent(new ChangeGameStateEvent(GameState.Playing));
        }

        public void ResetLevel(RestartEvent e)
        {
            EnemySpawnService.Instance.Reset();

            ProjectileSpawnService.Instance.Reset();

            ItemsService.Instance.Reset();

            PlayerCountersController.Instance.Reset();

            EventBus.RaiseEvent(new ChangeGameStateEvent(GameState.Playing));
        }
    }
}