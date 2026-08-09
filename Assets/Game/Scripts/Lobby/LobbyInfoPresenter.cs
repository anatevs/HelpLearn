using GameManagement;
using System;
using UI;

namespace Gameplay
{
    public class LobbyInfoPresenter : IDisposable
    {
        private readonly LobbyManager _lobbyManager;

        private readonly LobbyPlayersManager _lobbyPlayersManager;

        private readonly GameInfoPanel _gameInfoPanel;

        public LobbyInfoPresenter(LobbyManager lobbyManager,
            LobbyPlayersManager lobbyPlayersManager,
            GameInfoPanel gameInfoPanel)
        {
            _lobbyManager = lobbyManager;
            _lobbyPlayersManager = lobbyPlayersManager;
            _gameInfoPanel = gameInfoPanel;

            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _lobbyPlayersManager.OnPlayerAdded += HandleAddPlayer;

            _lobbyManager.OnServerPlayerDisconnected += HandlePlayerDisconnect;

            _lobbyManager.OnGamePlayerSpawned += HandleGamePlayerSpawn;
        }

        private void Unsubscribe()
        {
            _lobbyPlayersManager.OnPlayerAdded -= HandleAddPlayer;

            _lobbyManager.OnServerPlayerDisconnected -= HandlePlayerDisconnect;

            _lobbyManager.OnGamePlayerSpawned -= HandleGamePlayerSpawn;
        }


        private void HandleAddPlayer(int index, bool _)
        {
            var lobbyPlayer = _lobbyPlayersManager.GetPlayer(index);
            var playerName = lobbyPlayer.Name;

            _gameInfoPanel.AddLog($"Connected player: {playerName}");
            _gameInfoPanel.SetPlayersCount(_lobbyPlayersManager.Count);
        }

        private void HandlePlayerDisconnect(string playerName, int remainPlayers)
        {
            _gameInfoPanel.AddLog($"Disconnected player: {playerName}");
            _gameInfoPanel.SetPlayersCount(remainPlayers);
        }

        private void HandleGamePlayerSpawn(GamePlayer gamePlayer)
        {
            _gameInfoPanel.AddLog($"Spawned player: {gamePlayer.Name}");
        }
    }
}