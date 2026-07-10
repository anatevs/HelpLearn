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

        public void Clear()
        {
            _playersResults.Clear();
        }

        public void AddKills(string playerName, int value)
        {
            _playersResults[playerName].AddKills(value);
        }

        public void AddDeaths(string playerName, int value)
        {
            _playersResults[playerName].AddDeaths(value);
        }

        public List<PlayerResultsData> GetResultData()
        {
            return _playersResults.Values
                .OrderByDescending(x => x.Data.Score)
                .ThenBy(x => x.Data.Deaths)
                .ThenBy(x => x.Data.Name)
                .Select(x => x.Data)
                .ToList();
        }
    }
}