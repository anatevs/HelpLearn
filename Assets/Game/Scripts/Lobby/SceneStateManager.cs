using UnityEngine;
using Mirror;
using System;

namespace GameManagement
{
    public class SceneStateManager
    {
        public event Action<string> OnSceneLoadRequested;

        public bool IsStartLobby { get => _isEnterLobby; set => _isEnterLobby = value; }

        public bool IsInGameplay => Utils.IsSceneActive(_config.GameplayScene);

        public bool IsInLobby => Utils.IsSceneActive(_config.LobbyScene);

        public SceneStateManager(SceneConfig sceneConfig)
        {
            _config = sceneConfig;
        }

        private SceneConfig _config;

        private bool _isEnterLobby = true;

        public void Awake()
        {
            if (string.IsNullOrWhiteSpace(_config.LobbyScene))
            {
                Debug.LogError("NetworkRoomManager RoomScene is empty. Set the RoomScene in the inspector for the NetworkRoomManager");
                return;
            }

            if (string.IsNullOrWhiteSpace(_config.GameplayScene))
            {
                Debug.LogError("NetworkRoomManager PlayScene is empty. Set the PlayScene in the inspector for the NetworkRoomManager");
                return;
            }
        }

        public bool IsLobby(string sceneName)
        {
            return sceneName == _config.LobbyScene;
        }

        public bool IsGameplay(string sceneName)
        {
            return sceneName == _config.GameplayScene;
        }

        public void LoadGameScene()
        {
            OnSceneLoadRequested?.Invoke(_config.GameplayScene);
        }

        public void LoadLobby()
        {
            OnSceneLoadRequested?.Invoke(_config.LobbyScene);
        }
    }
}