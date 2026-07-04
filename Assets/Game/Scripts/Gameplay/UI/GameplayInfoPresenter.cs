using GameManagement;
using Mirror;
using System;
using System.Collections.Generic;

namespace UI
{
    public class GameplayInfoPresenter :
        IDisposable
    {
        private readonly GameInfoPanel _panel;
        private readonly List<GamePlayer> _players = new();

        public GameplayInfoPresenter()
        {
            if (NetworkManager.singleton is LobbyManager lobbyManager)
            {
                _panel = lobbyManager.GameInfoPanel;
            }
        }

        public void AddPlayer(GamePlayer player)
        {
            _players.Add(player);

            player.OnItemPicked += HandleItemPick;
        }

        public void Dispose()
        {
            foreach (GamePlayer player in _players)
            {
                if (player != null)
                {
                    player.OnItemPicked -= HandleItemPick;
                }
            }
        }

        private void HandleItemPick(string playerName, string itemName)
        {
            _panel.AddLog($"Player {playerName} picked item {itemName}");
        }
    }
}