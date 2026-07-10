using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LeaderboardStatView : MonoBehaviour
    {
        private readonly Dictionary<LeaderboardStatType, TMP_Text> _stats = new();

        [SerializeField]
        private StatViewData[] _statDataTexts =
            Enum.GetValues(typeof(LeaderboardStatType))
            .Cast<LeaderboardStatType>()
            .Select(type => new StatViewData(type, null))
            .ToArray();

        private void Awake()
        {
            _stats.Clear();

            foreach (var data in _statDataTexts)
            {
                _stats.Add(data.Type, data.Text);
            }
        }

        public void SetStat(LeaderboardStatType type, string text)
        {
            _stats[type].text = text;
        }
    }

    [Serializable]
    public struct StatViewData
    {
        public LeaderboardStatType Type;

        public TMP_Text Text;

        public StatViewData(LeaderboardStatType type, TMP_Text text)
        {
            Type = type;
            Text = text;
        }
    }
}