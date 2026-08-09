using Mirror;
using UnityEngine;
using GameManagement;
using UI;
using System.Collections.Generic;
using Gameplay;

namespace Network.UI
{
    public class LobbyHUDPresenter : NetworkBehaviour
    {
        [SerializeField]
        private LobbySlotsView _slotsView;

        [SerializeField]
        private StartGameView _startGameView;

        [SerializeField]
        private LeaderBoardView _leaderBoardView;

        [SerializeField]
        private string _readyStatus = "Ready";

        [SerializeField]
        private string _unreadyStatus = "Not ready";

        [SerializeField]
        private LobbyPlayerSettingsPresenter _playerSettingsPresenter;

        private LobbyManager _lobbyManager;

        private SceneStateManager _sceneStateManager;

        private LobbyPlayersManager _lobbyPlayersManager;

        private LobbyLeaderboardPresenter _leaderboardPresenter;

        private List<PlayerResultsData> _leaderboardData;

        private bool _isSubscribed = false;

        private void Awake()
        {
            if (NetworkManager.singleton is LobbyManager lobbyManager)
            {
                Init(lobbyManager, lobbyManager.GameInitializer);
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            _lobbyPlayersManager = _lobbyManager.LobbyPlayersManager;

            Subscribe();

            _startGameView.gameObject.SetActive(true);

            SetInitPlayers();

            ShowOrHideLeaderboard();
        }

        public override void OnStartClient()
        {
            if (!isServer)
            {
                base.OnStartClient();

                CmdRequestInit();
            }

            _playerSettingsPresenter.Show(_sceneStateManager.IsStartLobby);
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            _startGameView.gameObject.SetActive(false);
            _leaderboardPresenter.Hide();

            Unsubscribe();
        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            _leaderboardPresenter.Hide();
        }

        public void Init(LobbyManager lobbyManager, GameInitializer gameInitializer)
        {
            _lobbyManager = lobbyManager;

            var multiplayerConfig = _lobbyManager.MultiplayerSettingsConfig;

            _slotsView.Init(multiplayerConfig.MaxPlayers);

            _leaderboardPresenter = new LobbyLeaderboardPresenter(_leaderBoardView,
                    gameInitializer.LeaderboardStorage);

            _playerSettingsPresenter.Init(multiplayerConfig);

            _sceneStateManager = gameInitializer.SceneStateManager;
        }

        private void Subscribe()
        {
            if (!_isSubscribed)
            {
                _lobbyPlayersManager.OnPlayerAdded += ShowRemoveButton;
                _lobbyPlayersManager.OnPlayerRemoved += SetSlotEmpty;
                _lobbyPlayersManager.OnNameChanged += SetName;
                _lobbyPlayersManager.OnColorChanged += SetColor;
                _lobbyPlayersManager.OnReadyChanged += SetReadyStatus;
                _lobbyPlayersManager.OnSlotUpdated += UpdateSlot;

                _lobbyManager.OnCanStartChanged += _startGameView.EnableButton;

                _startGameView.OnStartClicked += HandleStartClick;
                _slotsView.OnRemoveClicked += _lobbyManager.DisconnectPlayer;

                _isSubscribed = true;
            }
        }

        private void Unsubscribe()
        {
            if (_lobbyManager != null && _isSubscribed)
            {
                if (_lobbyPlayersManager != null)
                {
                    _lobbyPlayersManager.OnPlayerAdded -= ShowRemoveButton;
                    _lobbyPlayersManager.OnPlayerRemoved -= SetSlotEmpty;
                    _lobbyPlayersManager.OnNameChanged -= SetName;
                    _lobbyPlayersManager.OnColorChanged -= SetColor;
                    _lobbyPlayersManager.OnReadyChanged -= SetReadyStatus;
                    _lobbyPlayersManager.OnSlotUpdated -= UpdateSlot;
                }

                _lobbyManager.OnCanStartChanged -= _startGameView.EnableButton;

                _startGameView.OnStartClicked -= HandleStartClick;
                _slotsView.OnRemoveClicked -= _lobbyManager.DisconnectPlayer;

                _isSubscribed = false;
            }
        }

        [Command(requiresAuthority = false)]
        private void CmdRequestInit()
        {
            SetInitPlayers();

            RpcShowLeaderboard(_leaderboardData);
        }

        private void SetInitPlayers()
        {
            for (int i = 0; i < _lobbyPlayersManager.Count; i++)
            {
                var player = _lobbyPlayersManager.GetPlayer(i);
                UpdateSlot(i, player.Name, player.Color, player.ReadyToBegin, player.isOwned);
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

        [Server]
        private void ShowOrHideLeaderboard()
        {
            _leaderboardData = _leaderboardPresenter.ShowOnServer();

            RpcShowLeaderboard(_leaderboardData);
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

        [ClientRpc]
        private void RpcShowLeaderboard(List<PlayerResultsData> data)
        {
            _leaderboardPresenter.Show(data);
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
            _sceneStateManager.LoadGameScene();
        }
    }
}