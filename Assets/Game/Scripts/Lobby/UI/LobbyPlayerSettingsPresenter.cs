using GameManagement;
using UnityEngine;

namespace Network.UI
{
    public class LobbyPlayerSettingsPresenter : MonoBehaviour
    {
        [SerializeField]
        private LobbyPlayerSettingsView _view;

        private LobbyPlayer _player;

        private MultiplayerSettingsConfig _config;

        public void Init(MultiplayerSettingsConfig settingsConfig)
        {
            _config = settingsConfig;
        }

        private void OnEnable()
        {
            LobbyPlayer.OnConnected += HandlePlayerConnected;
            LobbyPlayer.OnDisconnected += HandlePlayerDisconnected;
        }

        private void OnDisable()
        {
            LobbyPlayer.OnConnected -= HandlePlayerConnected;
            LobbyPlayer.OnDisconnected -= HandlePlayerDisconnected;
        }

        public void Show(bool isShow)
        {
            if (!isShow)
            {
                _view.Hide();
                return;
            }

            if (_player == null)
            {
                return;
            }

            _view.Show(_player.Name, _player.Color, _player.ReadyToBegin, _config.NameLengthRange);
        }

        private void HandlePlayerConnected(LobbyPlayer lobbyPlayer)
        {
            if (!lobbyPlayer.isLocalPlayer)
            {
                return;
            }

            _player = lobbyPlayer;

            _view.OnNameSetRequested += _player.CmdRequestNameChange;

            _view.OnColorSet += _player.CmdSetColor;

            _view.OnReadyChanged += _player.CmdChangeReadyState;

            _view.Show(_player.Name, _player.Color, _player.ReadyToBegin, _config.NameLengthRange);

            _player.OnNameChanged += HandleChangeName;
        }

        private void HandlePlayerDisconnected(LobbyPlayer lobbyPlayer)
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
                if (_player != null)
                {
                    _view.OnNameSetRequested -= _player.CmdRequestNameChange;

                    _view.OnColorSet -= _player.CmdSetColor;

                    _view.OnReadyChanged -= _player.CmdChangeReadyState;

                    _player.OnNameChanged -= HandleChangeName;
                }

                _view.Hide();
            }
        }

        private void HandleChangeName(int _, string name)
        {
            _view.SetNameTitle(name);
        }
    }
}