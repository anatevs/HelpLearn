using EventBusNamespace;
using GameManagement;
using UI;
using UnityEngine;

namespace Gameplay
{
    public sealed class SceneInitializer : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        [SerializeField]
        private PatrolZones _patrolZones;

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
            _patrolZones.Init();

            EnemySpawnService.Instance.Init(_player, _patrolZones.Locations);

            ProjectileSpawnService.Instance.Init();

            ItemsService.Instance.Init();

            PlayerCountersController.Instance.Init(_player);

            CanvasView.Instance.Init();

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