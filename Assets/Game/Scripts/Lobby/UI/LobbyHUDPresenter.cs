using Mirror;
using UnityEngine;
using GameManagement;

namespace Network.UI
{
    public class LobbyHUDPresenter : NetworkBehaviour
    {
        [SerializeField]
        private LobbySlotsView _slotsView;

        [SerializeField]
        private string _readyStatus = "Ready";

        [SerializeField]
        private string _unreadyStatus = "Not ready";

        private LobbyManager _lobbyManager;

        private bool _isSubscribed = false;

        public void Init(LobbyManager lobbyManager)
        {
            _lobbyManager = lobbyManager;

            _slotsView.Init(_lobbyManager.MultiplayerSettingsConfig.MaxPlayers);

            Subscribe();
        }

        private void OnDisable()
        {
            if (_lobbyManager != null)
            {
                _lobbyManager.OnPlayerRemoved -= SetSlotEmpty;
                _lobbyManager.OnNameChanged -= SetName;
                _lobbyManager.OnColorChanged -= SetColor;
                _lobbyManager.OnReadyChanged -= SetReadyStatus;
                _lobbyManager.OnPlayerUpdated -= UpdateSlot;
            }
        }

        private void Subscribe()
        {
            if (!_isSubscribed)
            {
                _lobbyManager.OnPlayerRemoved += SetSlotEmpty;
                _lobbyManager.OnNameChanged += SetName;
                _lobbyManager.OnColorChanged += SetColor;
                _lobbyManager.OnReadyChanged += SetReadyStatus;
                _lobbyManager.OnPlayerUpdated += UpdateSlot;

                _isSubscribed = true;
            }
        }

        [ClientRpc]
        private void SetName(int index, string name)
        {
            _slotsView.SetName(index, name);


            Debug.Log($"in HUD change name, number of players in manager {_lobbyManager.Players.Count}");
        }

        [ClientRpc]
        private void SetColor(int index, Color color)
        {
            _slotsView.SetColor(index, color);
        }

        [ClientRpc]
        private void UpdateSlot(int index, string name, Color color, bool isReady)
        {
            _slotsView.SetName(index, name);
            _slotsView.SetColor(index, color);

            if (isReady)
            {
                _slotsView.SetStatus(index, _readyStatus);
                return;
            }

            _slotsView.SetStatus(index, _unreadyStatus);
        }

        [ClientRpc]
        private void SetSlotEmpty(int index)
        {
            _slotsView.SetSlotEmpty(index);
        }

        [ClientRpc]
        private void SetReadyStatus(int index, bool ready)
        {
            if (ready)
            {
                _slotsView.SetStatus(index, _readyStatus);
                return;
            }

            _slotsView.SetStatus(index, _unreadyStatus);
        }
    }
}