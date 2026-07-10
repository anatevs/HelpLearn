using Gameplay;
using System.Collections.Generic;

namespace UI
{
    public class LobbyLeaderboardPresenter
    {
        private readonly LeaderBoardView _view;

        private readonly LeaderboardStorage _storage;

        public LobbyLeaderboardPresenter(LeaderBoardView view, LeaderboardStorage storage)
        {
            _view = view;
            _storage = storage;

            Hide();
        }

        public List<PlayerResultsData> ShowOnServer()
        {
            var results = _storage.GetResultData();

            Show(results);

            return results;
        }


        public void Show(List<PlayerResultsData> results)
        {
            if (results == null || results.Count == 0)
            {
                Hide();
                return;
            }

            _view.gameObject.SetActive(true);

            foreach (var result in results)
            {
                SetStat(result);
            }
        }

        public void Hide()
        {
            _view.gameObject.SetActive(false);
        }

        private void SetStat(PlayerResultsData data)
        {
            _view.SetStat(data.Name, LeaderboardStatType.Name, data.Name);
            _view.SetStat(data.Name, LeaderboardStatType.Kills, data.Kills.ToString());
            _view.SetStat(data.Name, LeaderboardStatType.Deaths, data.Deaths.ToString());
            _view.SetStat(data.Name, LeaderboardStatType.Score, data.Score.ToString());
        }
    }
}