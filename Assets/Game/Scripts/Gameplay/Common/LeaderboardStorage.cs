using System.Collections.Generic;
using System.Linq;

namespace Gameplay
{
    public class LeaderboardStorage
    {
        private readonly Dictionary<string, PlayerResults> _playersResults = new();

        private readonly int _killToScoreCoef = 100;

        public LeaderboardStorage(int killToScoreCoef)
        {
            _killToScoreCoef = killToScoreCoef;
        }

        public void AddPlayerName(string playerName)
        {
            var data = new PlayerResults(playerName, _killToScoreCoef);

            _playersResults.Add(playerName, data);
        }

        public void RemovePlayerName(string playerName)
        {
            _playersResults.Remove(playerName);
        }

        public void ResetData()
        {
            foreach (var player in _playersResults.Values)
            {
                player.ResetDataForPlayer();
            }
        }

        public void AddKills(string playerName, int value)
        {
            _playersResults[playerName].AddKills(value);
        }

        public void AddDeaths(string playerName, int value)
        {
            _playersResults[playerName].AddDeaths(value);
        }

        public IEnumerable<PlayerResults> GetResults()
        {
            return _playersResults.Values
                .OrderByDescending(x => x.Results.Score)
                .ThenBy(x => x.Results.Deaths)
                .ThenBy(x => x.Results.Name);
        }
    }
}