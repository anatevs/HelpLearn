using GameManagement;
using UnityEngine;

namespace Network.UI
{
    public class LobbyPlayerSettingsPresenter : MonoBehaviour
    {
        private LobbyPlayer _player;
        private LobbyPlayerSettingsView _view;

        private LobbyPlayerSettingsView _prefab;

        public void Init(MultiplayerSettingsConfig settingsConfig)
        {
            _prefab = settingsConfig.PlayerViewPrefab;

            Subscribe();
        }

        private void OnDisable()
        {
            LobbyPlayer.OnStarted -= HandlePlayerStarted;
            LobbyPlayer.OnRemoved -= HandlePlayerRemove;

            DisableView();
        }

        private void Subscribe()
        {
            LobbyPlayer.OnStarted += HandlePlayerStarted;
            LobbyPlayer.OnRemoved += HandlePlayerRemove;
        }

        private LobbyPlayerSettingsView CreateView()
        {
            var view = Instantiate(_prefab, transform);

            return view;
        }

        private void HandlePlayerStarted(LobbyPlayer lobbyPlayer)
        {
            var view = CreateView();

            if (!lobbyPlayer.isLocalPlayer)
            {
                return;
            }

            _player = lobbyPlayer;
            _view = view;

            view.OnNameSet += _player.CmdSetName;

            view.OnColorSet += _player.CmdSetColor;

            view.OnReadyChanged += _player.CmdChangeReadyState;

            view.Show(lobbyPlayer.Name, lobbyPlayer.Color);
        }

        private void HandlePlayerRemove(LobbyPlayer lobbyPlayer)
        {
            if (!lobbyPlayer.isLocalPlayer)
            {
                return;
            }

            DisableView();
        }

        private void DisableView()
        {
            if (_view != null)
            {
                _view.OnNameSet -= _player.CmdSetName;

                _view.OnColorSet -= _player.CmdSetColor;

                _view.OnReadyChanged -= _player.CmdChangeReadyState;

                _view.Hide();

                Destroy(_view.gameObject);
            }
        }
    }
}