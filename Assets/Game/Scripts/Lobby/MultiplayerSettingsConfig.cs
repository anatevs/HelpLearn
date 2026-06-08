using Network.UI;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "MultiplayerConfig",
        menuName = "Configs/Multiplayer/MultiplayerSettings")]
    public class MultiplayerSettingsConfig : ScriptableObject
    {
        public int MinPlayers => _minPlayers;
        public int MaxPlayers => _maxPlayers;
        public string DefaultNamePrefix => _defaultNamePrefix;
        public Color DefaultColor => _defaultColor;

        public LobbyPlayerSettingsView PlayerViewPrefab => _playerViewPrefab;

        [SerializeField]
        private int _minPlayers = 2;

        [SerializeField]
        private int _maxPlayers = 4;

        [SerializeField]
        private string _defaultNamePrefix = "Player";

        [SerializeField]
        private Color _defaultColor = Color.white;

        [SerializeField]
        private LobbyPlayerSettingsView _playerViewPrefab;
    }
}