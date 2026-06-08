using Mirror;
using Network.UI;
using UnityEngine;

namespace GameManagement
{
    public class NetworkLobbyPlayer : NetworkRoomPlayer
    {
        [SerializeField]
        private LobbyPlayerSettingsView _uiPrefab;

        private LobbyPlayerSettingsView _settingsView;

        [SyncVar]
        public string Name;

        [SyncVar]
        public Color Color = Color.black;

        private bool _disabled = false;

        private NetworkLobbyManager _lobbyManager;


        public override void Start()
        {
            base.Start();

            _lobbyManager = (NetworkLobbyManager)NetworkManager.singleton;

            if (!isLocalPlayer)
            {
                return;
            }

            if (string.IsNullOrEmpty(Name))
            {
                Name = $"Player {index}";
            }

            name = Name;

            var canvas = FindAnyObjectByType<Canvas>();

            _settingsView = GameObject.Instantiate(_uiPrefab.gameObject, canvas.transform)
                .GetComponent<LobbyPlayerSettingsView>();

            _settingsView.OnNameSet += CmdSetName;

            _settingsView.OnColorSet += CmdSetColor;

            _settingsView.Show(Name, Color);

            CmdSetName(Name);
            CmdSetColor(Color);
            CmdUpdateClients();
        }


        //public override void OnStartClient()
        //{
        //    base.OnStartClient();

        //    _lobbyManager = (NetworkLobbyManager)NetworkManager.singleton;

        //    if (!isLocalPlayer)
        //    {
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(Name))
        //    {
        //        Name = $"Player {index}";
        //    }

        //    name = Name;

        //    var canvas = FindAnyObjectByType<Canvas>();

        //    _settingsView = GameObject.Instantiate(_uiPrefab.gameObject, canvas.transform)
        //        .GetComponent<LobbyPlayerSettingsView>();

        //    _settingsView.OnNameSet += CmdSetName;

        //    _settingsView.OnColorSet += CmdSetColor;

        //    _settingsView.Show(Name, Color);

        //    CmdSetName(Name);
        //    CmdSetColor(Color);
        //}

        public override void OnStopLocalPlayer()
        {
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
            if (!_disabled && isLocalPlayer && _settingsView != null)
            {
                _disabled = true;

                _settingsView.OnNameSet -= CmdSetName;
                _settingsView.OnColorSet -= CmdSetColor;

                _settingsView.Hide();
            }
        }

        [Command]
        private void CmdSetName(string name)
        {
            Name = name;
            _lobbyManager.ChangeName(index, name);
        }

        [Command]
        private void CmdSetColor(Color color)
        {
            Color = color;
            _lobbyManager.ChangeColor(index, color);
        }

        [Command]
        private void CmdUpdateClients()
        {
            _lobbyManager.UpdateClients();
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