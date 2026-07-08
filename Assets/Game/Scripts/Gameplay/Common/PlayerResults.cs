namespace Gameplay
{
    public class PlayerResults
    {
        public PlayerResultsData Results => _data;

        private PlayerResultsData _data = new("", 0, 0, 0);

        private readonly int _killToScoreCoef = 100;

        public PlayerResults(string playerName, int killToScoreCoef)
        {
            _data.Name = playerName;
            _killToScoreCoef = killToScoreCoef;
        }

        public void ResetDataForPlayer()
        {
            _data.Deaths = 0;
            _data.Kills = 0;
            _data.Score = 0;
        }

        public void AddKills(int value)
        {
            if (value < 0)
            {
                return;
            }

            _data.Kills += value;

            _data.Score += value * _killToScoreCoef;
        }

        public void AddDeaths(int value)
        {
            if (value < 0)
            {
                return;
            }

            _data.Deaths += value;
        }
    }

    public struct PlayerResultsData
    {
        public string Name;
        public int Kills;
        public int Deaths;
        public int Score;

        public PlayerResultsData(string playerName,
            int kills, int deaths, int score)
        {
            Name = playerName;
            Kills = kills;
            Deaths = deaths;
            Score = score;
        }
    }
}