using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "UITitlesConfig",
        menuName = "Configs/UITitles")]
    public class UITitlesConfig : ScriptableObject
    {
        public string ProjectilesTitle => _projectiles;
        public string TargetsTitle => _targets;

        [SerializeField]
        private string _projectiles;

        [SerializeField]
        private string _targets;
    }
}