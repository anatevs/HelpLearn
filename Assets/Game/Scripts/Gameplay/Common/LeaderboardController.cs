using GameManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class LeaderboardController :
        IDisposable
    {
        private readonly LeaderboardStorage _leaderboardStorage;

        private readonly List<GamePlayer> _players = new();

        public LeaderboardController(LeaderboardStorage leaderboardStorage)
        {
            _leaderboardStorage = leaderboardStorage;
        }

        public void AddPlayer(GamePlayer player)
        {
            _leaderboardStorage.AddPlayerName(player.Name);

            _players.Add(player);

            player.OnKilled += HandlePlayerKill;
        }

        public void RemovePlayer(GamePlayer player)
        {
            if (player != null)
            {
                player.OnKilled -= HandlePlayerKill;

                _leaderboardStorage.RemovePlayerName(player.Name);
                _players.Remove(player);
            }
        }

        public void Dispose()
        {
            foreach (var player in _players)
            {
                if (player != null)
                {
                    player.OnKilled -= HandlePlayerKill;
                }
            }
        }

        private void HandlePlayerKill(GamePlayer player, string killerName)
        {
            _leaderboardStorage.AddKills(killerName, 1);
            _leaderboardStorage.AddDeaths(player.Name, 1);

            //Debug.Log($"leaderboard updated");

            //foreach (var result in _leaderboardStorage.GetResults())
            //{
            //    var data = result.Results;
            //    Debug.Log($"{data.Name} - kills: {data.Kills}, deaths: {data.Deaths}, score {data.Score}");
            //}
        }
    }
}