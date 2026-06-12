using GameManagement;
using UnityEngine;

namespace Network.UI
{
    public class LobbyPlayerSettingsPresenter : MonoBehaviour
    {
        private LobbyPlayer _player;
        private LobbyPlayerSettingsView _view;

        private MultiplayerSettingsConfig _config;

        public void Init(MultiplayerSettingsConfig settingsConfig)
        {
            _config = settingsConfig;

            Subscribe();
        }

        private void OnDisable()
        {
            LobbyPlayer.OnStarted -= HandlePlayerStarted;
            LobbyPlayer.OnStopped -= HandlePlayerRemove;

            DisableView();
        }

        private void Subscribe()
        {
            LobbyPlayer.OnStarted += HandlePlayerStarted;
            LobbyPlayer.OnStopped += HandlePlayerRemove;
        }

        private LobbyPlayerSettingsView CreateView()
        {
            var view = Instantiate(_config.PlayerViewPrefab, transform);

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

            view.OnNameSetRequested += _player.CmdRequestNameChange;

            view.OnColorSet += _player.CmdSetColor;

            view.OnReadyChanged += _player.CmdChangeReadyState;

            view.Show(lobbyPlayer.Name, lobbyPlayer.Color, _config.NameLengthRange);

            _player.OnNameChanged += HandleChangeName;
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
                _view.OnNameSetRequested -= _player.CmdRequestNameChange;

                _view.OnColorSet -= _player.CmdSetColor;

                _view.OnReadyChanged -= _player.CmdChangeReadyState;

                _view.Hide();

                _player.OnNameChanged -= HandleChangeName;

                Destroy(_view.gameObject);
            }
        }

        private void HandleChangeName(int _, string name)
        {
            _view.SetNameTitle(name);
        }
    }
}