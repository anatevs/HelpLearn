using UnityEngine;

namespace GameManagement
{
    public class MultiplayerSettingsConfig : ScriptableObject
    {
        public int MinPlayers => _minPlayers;
        public int MaxPlayers => _maxPlayers;

        [SerializeField]
        private int _minPlayers = 2;

        [SerializeField]
        private int _maxPlayers = 4;
    }
}