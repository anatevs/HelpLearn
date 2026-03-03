using EventBusNamespace;
using UnityEngine;

namespace GameManagement
{
    public class SoundPlayer : DDOLClass<SoundPlayer>
    {
        [SerializeField]
        private SoundsConfig _config;

        [SerializeField]
        private AudioSource _audioSource;

        private void Awake()
        {
            _config.Init();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<EnemyKilledEvent>(PlayEnemyKilled);
            EventBus.Subscribe<ChangeGameStateEvent>(PlayWin);
            EventBus.Subscribe<ChangeGameStateEvent>(PlayLose);
            EventBus.Subscribe<ItemPickedEvent>(PlayPickItem);
            EventBus.Subscribe<ShotEvent>(PlayShot);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<EnemyKilledEvent>(PlayEnemyKilled);
            EventBus.Unsubscribe<ChangeGameStateEvent>(PlayWin);
            EventBus.Unsubscribe<ChangeGameStateEvent>(PlayLose);
            EventBus.Unsubscribe<ItemPickedEvent>(PlayPickItem);
            EventBus.Unsubscribe<ShotEvent>(PlayShot);
        }

        private void PlayEnemyKilled(EnemyKilledEvent e)
        {
            PlaySound(SoundType.KillEnemy);
        }

        private void PlayWin(ChangeGameStateEvent e)
        {
            if (e.Value == GameStateType.Win)
            {
                PlaySound(SoundType.Win);
            }
        }

        private void PlayLose(ChangeGameStateEvent e)
        {
            if (e.Value == GameStateType.Lose)
            {
                PlaySound(SoundType.Lose);
            }
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


        [SerializeField]
        private bool _isPlay;
        [SerializeField]
        private SoundType _type;
        private void Update()
        {
            if (_isPlay)
            {
                _isPlay = false;
                PlaySound(_type);
            }
        }

    }
}