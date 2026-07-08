using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "MatchConfig",
        menuName = "Configs/Match")]
    public class MatchConfig : ScriptableObject
    {
        public float MatchTime => _matchTimeSeconds;

        public int KillToScoreCoef => _killToScoreCoef;

        [SerializeField]
        private float _matchTimeSeconds = 300f;

        [SerializeField]
        private int _killToScoreCoef = 100;
    }
}