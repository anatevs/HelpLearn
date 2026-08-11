using Mirror;
using System;
using UnityEngine;

namespace GameManagement
{
    public class LobbyPlayer : NetworkBehaviour
    {
        public static event Action<LobbyPlayer> OnConnected;
        public static event Action<LobbyPlayer> OnDisconnected;

        public event Action<int, string> OnNameChanged;
        public event Action<int, Color> OnColorChanged;
        public event Action<int, bool> OnReadyChanged;
        public event Action<int, string> OnNameChangeRequested;

        [SyncVar(hook = nameof(HookSetName))]
        public string Name;

        [SyncVar]
        public Color Color = Color.white;

        [SyncVar]
        public int PlayerID;

        [SyncVar]
        public bool ReadyToBegin = false;

        public void Init(string name)
        {
            Name = name;
            this.name = name;
            OnNameChanged?.Invoke(PlayerID, name);

            Color = Color.white;

            ReadyToBegin = false;
        }

        public virtual void Start()
        {
            // LobbyPlayer object must be set to DontDestroyOnLoad along with LobbyManager
            // in server and all clients, otherwise it will be respawned in the game scene which would
            // have undesirable effects.
            DontDestroyOnLoad(gameObject);
        }


        public override void OnStartClient()
        {
            base.OnStartClient();

            if (isLocalPlayer)
            {
                OnConnected?.Invoke(this);
            }
        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            if (isLocalPlayer)
            {
                OnDisconnected?.Invoke(this);
            }
        }

        #region Hooks

        public void HookSetName(string oldName, string newName)
        {
            this.name = newName;
            OnNameChanged?.Invoke(PlayerID, newName);
        }

        #endregion


        #region Commands

        [Command]
        public void CmdRequestNameChange(string newName)
        {
            OnNameChangeRequested?.Invoke(PlayerID, newName);
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
        }

        #endregion
    }
}