using System.Collections;
using UnityEngine;

namespace NewsFeed
{
    [CreateAssetMenu(
        fileName = "SpinnerConfig",
        menuName = "Configs/Spinner")]
    public class SpinnerConfig : ScriptableObject
    {
        public string[] CharSequence => _charSequence;

        public float OneCharDelay => _oneCharDelay;

        [SerializeField]
        private string[] _charSequence;

        [SerializeField]
        private float _oneCharDelay;
    }
}