using Mirror;
using System;
using UnityEngine;

namespace GameManagement
{
    public class LobbyPlayer : NetworkBehaviour
    {
        public static event Action<LobbyPlayer> OnStarted;
        public static event Action<LobbyPlayer> OnRemoved;

        public event Action<int, string> OnNameChanged;
        public event Action<int, Color> OnColorChanged;
        public event Action<int, bool> OnReadyChanged;

        [SyncVar(hook = nameof(SetObjectName))]
        public string Name;

        [SyncVar]
        public Color Color = Color.black;

        [SyncVar]
        public int PlayerID;

        [SyncVar]
        public bool ReadyToBegin = false;

        private bool _disabled = false;

        public void Init(string name)
        {
            Name = name;
            this.name = name;
        }

        public override void OnStartClient()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            OnStarted?.Invoke(this);

            CmdSetName(Name);
            CmdSetColor(Color);
        }

        public override void OnStopClient()
        {
            OnRemoved?.Invoke(this);
        }

        #region Hooks

        public void SetObjectName(string oldName, string newName)
        {
            this.name = newName;
        }

        #endregion


        #region Commands

        [Command]
        public void CmdSetName(string name)
        {
            Name = name;
            this.name = name;

            OnNameChanged?.Invoke(PlayerID, name);
        }

        [Command]
        public void CmdSetColor(Color color)
        {
            Color = color;

            OnColorChanged?.Invoke(PlayerID, color);
        }

        [Command]
        public void CmdChangeReadyState(bool readyState)
        {
            ReadyToBegin = readyState;

            OnReadyChanged?.Invoke(PlayerID, readyState);

            //NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
            //if (room != null)
            //{
            //    room.ReadyStatusChanged();
            //}
        }

        #endregion


        #region Optional UI
        //public void OnGUI()
        //{
        //    LobbyManager room = NetworkManager.singleton as LobbyManager;
        //    if (room)
        //    {
        //        //if (!room.showRoomGUI)
        //        //    return;

        //        if (!Utils.IsSceneActive(room.RoomScene))
        //            return;

        //        DrawPlayerReadyState();
        //        DrawPlayerReadyButton();
        //    }
        //}

        //void DrawPlayerReadyState()
        //{
        //    GUILayout.BeginArea(new Rect(20f + (PlayerID * 100), 200f, 90f, 130f));

        //    GUI.color = Color;
        //    GUILayout.Label($"{Name}");
        //    GUI.color = Color.white;

        //    if (ReadyToBegin)
        //        GUILayout.Label("Ready");
        //    else
        //        GUILayout.Label("Not Ready");

        //    if (((isServer && PlayerID > 0) || isServerOnly) && GUILayout.Button("REMOVE"))
        //    {
        //        // This button only shows on the Host for all players other than the Host
        //        // Host and Players can't remove themselves (stop the client instead)
        //        // Host can kick a Player this way.
        //        GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
        //    }

        //    GUILayout.EndArea();
        //}

        //void DrawPlayerReadyButton()
        //{
        //    if (NetworkClient.active && isLocalPlayer)
        //    {
        //        GUILayout.BeginArea(new Rect(20f, 300f, 120f, 20f));

        //        if (ReadyToBegin)
        //        {
        //            if (GUILayout.Button("Cancel"))
        //                CmdChangeReadyState(false);
        //        }
        //        else
        //        {
        //            if (GUILayout.Button("Ready"))
        //                CmdChangeReadyState(true);
        //        }

        //        GUILayout.EndArea();
        //    }
        //}

        #endregion
    }
}