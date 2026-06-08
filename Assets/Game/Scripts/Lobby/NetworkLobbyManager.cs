using Mirror;
using Network.UI;
using System;
using UnityEngine;

namespace GameManagement
{
    public class NetworkLobbyManager : NetworkRoomManager
    {
        public event Action<int, string> OnNameChanged;
        public event Action<int, Color> OnColorChanged;
        public event Action OnClientsUpdated;

        public MultiplayerSettingsConfig MultiplayerSettingsConfig => _settingsConfig;

        [Header("Configs")]
        [SerializeField]
        private MultiplayerSettingsConfig _settingsConfig;

        private bool _showStartButton;

        public void ChangeName(int index, string name)
        {
            OnNameChanged?.Invoke(index, name);
        }

        public void ChangeColor(int index, Color color)
        {
            OnColorChanged?.Invoke(index, color);
        }

        public void UpdateClients()
        {
            OnClientsUpdated?.Invoke();
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
        }

        public override void OnRoomServerPlayersReady()
        {
            if (Utils.IsHeadless())
            {
                base.OnRoomServerPlayersReady();
            }
            else
            {
                _showStartButton = true;
            }
        }

        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            if (roomPlayer.TryGetComponent<NetworkLobbyPlayer>(out var lobbyPlayer))
            {
                lobbyPlayer.DisableLobbyPlayer();

                if (gamePlayer.TryGetComponent<NetworkPlayer>(out var player))
                {
                    player.Name = lobbyPlayer.Name;
                    player.Color = lobbyPlayer.Color;
                }
            }

            return true;
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if (allPlayersReady && _showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
            {
                _showStartButton = false;

                ServerChangeScene(GameplayScene);
            }
        }
    }
}