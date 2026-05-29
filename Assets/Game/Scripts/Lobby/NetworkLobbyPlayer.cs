using Mirror;
using Network.UI;
using UnityEngine;

namespace GameManagement
{
    public class NetworkLobbyPlayer : NetworkRoomPlayer
    {
        private LobbyPlayerSettingsView _settingsView;

        [SyncVar]
        public string Name;

        [SyncVar]
        public Color Color = Color.black;

        private bool _disabled = false;

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (string.IsNullOrEmpty(Name))
            {
                Name = $"Player {index}";
            }

            name = Name;

            if (!isLocalPlayer)
            {
                Debug.Log($"is local player {name}");
                return;
            }

            _settingsView = GetComponentInChildren<LobbyPlayerSettingsView>(true);

            _settingsView.OnNameSet += CmdSetName;

            _settingsView.OnColorSet += CmdSetColor;

            _settingsView.Show("", Color);
        }

        public override void OnStopLocalPlayer()
        {
            Debug.Log($"local disable {Name}");

            base.OnStopLocalPlayer();

            DisableLobbyPlayer();
        }

        public override void OnStopServer()
        {
            base.OnStopServer();

            DisableLobbyPlayer();
        }

        public void DisableLobbyPlayer()
        {
            if (!_disabled)
            {
                _disabled = true;

                if (_settingsView == null)
                {
                    return;
                }

                _settingsView.OnNameSet -= CmdSetName;
                _settingsView.OnColorSet -= CmdSetColor;

                _settingsView.Hide();

                Debug.Log($"stop lobby player {Name}");
            }
        }


        [Command]
        private void CmdSetName(string name)
        {
            Name = name;
        }

        [Command]
        private void CmdSetColor(Color color)
        {
            Color = color;
        }

        #region Optional UI

        /// <summary>
        /// Render a UI for the room. Override to provide your own UI
        /// </summary>
        public override void OnGUI()
        {
            if (!showRoomGUI)
                return;

            NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
            if (room)
            {
                if (!room.showRoomGUI)
                    return;

                if (!Utils.IsSceneActive(room.RoomScene))
                    return;

                DrawPlayerReadyState();
                DrawPlayerReadyButton();
            }
        }

        void DrawPlayerReadyState()
        {
            GUILayout.BeginArea(new Rect(20f + (index * 100), 200f, 90f, 130f));

            GUI.color = Color;
            GUILayout.Label($"{Name}");
            GUI.color = Color.white;

            if (readyToBegin)
                GUILayout.Label("Ready");
            else
                GUILayout.Label("Not Ready");

            if (((isServer && index > 0) || isServerOnly) && GUILayout.Button("REMOVE"))
            {
                // This button only shows on the Host for all players other than the Host
                // Host and Players can't remove themselves (stop the client instead)
                // Host can kick a Player this way.
                GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
            }

            GUILayout.EndArea();
        }

        void DrawPlayerReadyButton()
        {
            if (NetworkClient.active && isLocalPlayer)
            {
                GUILayout.BeginArea(new Rect(20f, 300f, 120f, 20f));

                if (readyToBegin)
                {
                    if (GUILayout.Button("Cancel"))
                        CmdChangeReadyState(false);
                }
                else
                {
                    if (GUILayout.Button("Ready"))
                        CmdChangeReadyState(true);
                }

                GUILayout.EndArea();
            }
        }

        #endregion
    }
}