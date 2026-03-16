using EventBusNamespace;
using UnityEngine;

namespace GameManagement
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private SoundsConfig _config;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private EventBus _eventBus;

        private void Awake()
        {
            _config.Init();
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<EnemyKilledEvent>(PlayEnemyKilled);
            _eventBus.Subscribe<GameWinEvent>(PlayWin);
            _eventBus.Subscribe<GameLoseEvent>(PlayLose);
            _eventBus.Subscribe<ItemPickedEvent>(PlayPickItem);
            _eventBus.Subscribe<ShotEvent>(PlayShot);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<EnemyKilledEvent>(PlayEnemyKilled);
            _eventBus.Unsubscribe<GameWinEvent>(PlayWin);
            _eventBus.Unsubscribe<GameLoseEvent>(PlayLose);
            _eventBus.Unsubscribe<ItemPickedEvent>(PlayPickItem);
            _eventBus.Unsubscribe<ShotEvent>(PlayShot);
        }

        private void PlayEnemyKilled(EnemyKilledEvent e)
        {
            PlaySound(SoundType.KillEnemy);
        }

        private void PlayWin(GameWinEvent e)
        {
            PlaySound(SoundType.Win);
        }

        private void PlayLose(GameLoseEvent e)
        {
            PlaySound(SoundType.Lose);
        }

        private void PlayPickItem(ItemPickedEvent e)
        {
            PlaySound(SoundType.PickItem);
        }

        private void PlayShot(ShotEvent e)
        {
            PlaySound(SoundType.Shot);
        }

        public void PlaySound(SoundType type)
        {
            _audioSource.PlayOneShot(_config.GetSound(type));
        }
    }
}