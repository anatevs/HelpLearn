using Mirror;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "SceneConfig",
        menuName = "Configs/Scenes")]
    public class SceneConfig : ScriptableObject
    {
        public string LobbyScene => _lobbyScene;
        public string GameplayScene => _gameplayScene;

        [Header("Scenes")]
        /// <summary>
        /// The scene to use for the room. This is similar to the offlineScene of the NetworkManager.
        /// </summary>
        [Scene, SerializeField]
        private string _lobbyScene;

        /// <summary>
        /// The scene to use for the playing the game from the room. This is similar to the onlineScene of the NetworkManager.
        /// </summary>
        [Scene, SerializeField]
        private string _gameplayScene;
    }
}