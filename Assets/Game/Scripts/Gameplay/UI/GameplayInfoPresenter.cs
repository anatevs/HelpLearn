using GameManagement;
using Gameplay;
using System;
using System.Collections.Generic;

namespace UI
{
    public class GameplayInfoPresenter :
        IDisposable
    {
        private readonly GameInfoPanel _panel;
        private readonly List<GamePlayer> _players = new();

        private readonly MatchTimer _matchTimer;

        public GameplayInfoPresenter(GameInfoPanel gameInfoPanel, MatchTimer matchTimer)
        {
            _panel = gameInfoPanel;

            _matchTimer = matchTimer;
            _matchTimer.OnTimerStarted += HandleMatchStart;
        }

        public void AddPlayer(GamePlayer player)
        {
            _players.Add(player);

            player.OnItemPicked += HandleItemPick;
        }

        public void Dispose()
        {
            if (_matchTimer != null)
            {
                _matchTimer.OnTimerStarted -= HandleMatchStart;
            }

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

        private void HandleMatchStart()
        {
            _panel.AddLog($"Match started!");
        }
    }
}