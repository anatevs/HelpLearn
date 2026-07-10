using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class LeaderBoardView : MonoBehaviour
    {
        [SerializeField]
        private LeaderboardStatView _viewPrefab;

        private readonly Dictionary<string, LeaderboardStatView> _playerViews = new();

        public void AddStat(string playerName)
        {
            var statView = Instantiate(_viewPrefab, transform);
            _playerViews[playerName] = statView;
        }

        public void RemoveStat(string playerName)
        {
            var statView = _playerViews[playerName];
            _playerViews.Remove(playerName);
            Destroy(statView.gameObject);
        }

        public void SetStat(string playerName, LeaderboardStatType statType, string statText)
        {
            if (!_playerViews.ContainsKey(playerName))
            {
                AddStat(playerName);
            }

            _playerViews[playerName].SetStat(statType, statText);
        }
    }
}