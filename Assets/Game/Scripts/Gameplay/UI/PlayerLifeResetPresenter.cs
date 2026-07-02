using GameManagement;
using Mirror;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class PlayerLifeResetPresenter : MonoBehaviour
    {
        [SerializeField]
        private PlayerLifeResetView _view;

        private GamePlayer _player;

        private float _respawnDelay;

        public void Init(GamePlayer player)
        {
            _player = player;

            _respawnDelay = _player.Health.HPConfig.RespawnDelay;

            _player.OnRespawnCooldownStarted += ShowResetBar;
            _player.OnRespawnCooldownCompleted += HideResetBar;
        }

        public void OnDisable()
        {
            if (_player != null)
            {
                _player.OnRespawnCooldownStarted -= ShowResetBar;
                _player.OnRespawnCooldownCompleted -= HideResetBar;
            }
        }

        private void ShowResetBar(float endTime)
        {
            _view.SetProgress(0);
            _view.gameObject.SetActive(true);

            StartCoroutine(ShowProgress(endTime));
        }

        private void HideResetBar()
        {
            _view.gameObject.SetActive(false);
        }

        private IEnumerator ShowProgress(float endTime)
        {
            var progress = 0f;

            while (progress <= 1)
            {
                _view.SetProgress(progress);
                progress = 1 - (float)(endTime - NetworkTime.time) / _respawnDelay;

                yield return null;
            }
        }
    }
}