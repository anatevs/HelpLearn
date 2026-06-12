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
        private StartGameView _startGameView;

        [SerializeField]
        private string _readyStatus = "Ready";

        [SerializeField]
        private string _unreadyStatus = "Not ready";

        private LobbyManager _lobbyManager;

        private bool _isSubscribed = false;

        private void Awake()
        {
            if (NetworkManager.singleton is LobbyManager lobbyManager)
            {
                Init(lobbyManager);
            }
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            if (_lobbyManager != null)
            {
                _lobbyManager.OnPlayerAdded -= ShowRemoveButton;
                _lobbyManager.OnSlotEmptied -= SetSlotEmpty;
                _lobbyManager.OnNameChanged -= SetName;
                _lobbyManager.OnColorChanged -= SetColor;
                _lobbyManager.OnReadyChanged -= SetReadyStatus;
                _lobbyManager.OnSlotUpdated -= UpdateSlot;

                _lobbyManager.OnCanStartChanged -= _startGameView.EnableButton;

                _startGameView.OnStartClicked -= HandleStartClick;
                _slotsView.OnRemoveClicked -= _lobbyManager.DisconnectPlayer;

                _isSubscribed = false;
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            _startGameView.gameObject.SetActive(true);
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            _startGameView.gameObject.SetActive(false);
        }

        public void Init(LobbyManager lobbyManager)
        {
            _lobbyManager = lobbyManager;

            _slotsView.Init(_lobbyManager.MultiplayerSettingsConfig.MaxPlayers);

            Subscribe();
        }

        private void Subscribe()
        {
            if (!_isSubscribed)
            {
                _lobbyManager.OnPlayerAdded += ShowRemoveButton;
                _lobbyManager.OnSlotEmptied += SetSlotEmpty;
                _lobbyManager.OnNameChanged += SetName;
                _lobbyManager.OnColorChanged += SetColor;
                _lobbyManager.OnReadyChanged += SetReadyStatus;
                _lobbyManager.OnSlotUpdated += UpdateSlot;

                _lobbyManager.OnCanStartChanged += _startGameView.EnableButton;

                _startGameView.OnStartClicked += HandleStartClick;
                _slotsView.OnRemoveClicked += _lobbyManager.DisconnectPlayer;

                _isSubscribed = true;
            }
        }

        [Server]
        private void ShowRemoveButton(int index, bool server)
        {
            _slotsView.ShowRemoveButton(index, !server);
        }

        [Server]
        private void SetName(int index, string name)
        {
            SetNameLogic(index, name);

            RpcSetName(index, name);
        }

        [Server]
        private void SetColor(int index, Color color)
        {
            SetColorLogic(index, color);

            RpcSetColor(index, color);
        }

        [Server]
        private void UpdateSlot(int index, string name, Color color, bool isReady, bool server)
        {
            UpdateSlotLogic(index, name, color, isReady);

            RpcUpdateSlot(index, name, color, isReady);

            ShowRemoveButton(index, server);
        }

        [Server]
        private void SetSlotEmpty(int index)
        {
            SetSlotEmptyLogic(index);

            RpcSetSlotEmpty(index);
        }

        [Server]
        private void SetReadyStatus(int index, bool isReady)
        {
            SetReadyStatusLogic(index, isReady);

            RpcSetReadyStatus(index, isReady);
        }

        [ClientRpc]
        private void RpcSetName(int index, string name)
        {
            SetNameLogic(index, name);
        }

        [ClientRpc]
        private void RpcSetColor(int index, Color color)
        {
            SetColorLogic(index, color);
        }

        [ClientRpc]
        private void RpcUpdateSlot(int index, string name, Color color, bool isReady)
        {
            UpdateSlotLogic(index, name, color, isReady);
        }

        [ClientRpc]
        private void RpcSetSlotEmpty(int index)
        {
            SetSlotEmptyLogic(index);
        }

        [ClientRpc]
        private void RpcSetReadyStatus(int index, bool ready)
        {
            SetReadyStatusLogic(index, ready);
        }


        private void SetNameLogic(int index, string name)
        {
            _slotsView.SetName(index, name);
        }

        private void SetColorLogic(int index, Color color)
        {
            _slotsView.SetColor(index, color);
        }

        private void UpdateSlotLogic(int index, string name, Color color, bool isReady)
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

        private void SetSlotEmptyLogic(int index)
        {
            _slotsView.SetSlotEmpty(index);
        }

        private void SetReadyStatusLogic(int index, bool isReady)
        {
            if (isReady)
            {
                _slotsView.SetStatus(index, _readyStatus);
                return;
            }

            _slotsView.SetStatus(index, _unreadyStatus);
        }

        private void HandleStartClick()
        {
            _lobbyManager.LoadGameScene();
        }
    }
}