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
        public int[] NameLengthRange => _nameLengthRange;
        public Color DefaultColor => _defaultColor;

        public LobbyPlayerSettingsView PlayerViewPrefab => _playerViewPrefab;

        private void OnValidate()
        {
            if ((_minPlayers <= 0) || (_maxPlayers < _minPlayers))
            {
                Debug.LogError("min and max players must be more then 0; max must be more than min");
            }

            if (_nameLengthRange.Length < 2 || _nameLengthRange[0] <= 0 || _nameLengthRange[0] > _nameLengthRange[1])
            {
                Debug.LogError($"name length range must contain at least 2 elements, both > 0 and 0th must be <= 1st");
            }
        }

        [SerializeField]
        private int _minPlayers = 2;

        [SerializeField]
        private int _maxPlayers = 4;

        [SerializeField]
        private string _defaultNamePrefix = "Player";

        [SerializeField]
        private int[] _nameLengthRange = { 2, 4 };

        [SerializeField]
        private Color _defaultColor = Color.white;

        [SerializeField]
        private LobbyPlayerSettingsView _playerViewPrefab;
    }
}