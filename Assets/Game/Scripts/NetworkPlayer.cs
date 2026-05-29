using Gameplay;
using Mirror;
using UnityEngine;

namespace GameManagement
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SyncVar(hook = nameof(SetName))]
        public string Name = "nameDefault";

        [SyncVar(hook = nameof(SetColor))]
        public Color Color = Color.black;

        [SerializeField]
        private PlayerLooking _playerLooking;

        private void Start()
        {
            _playerLooking.SetName(Name);

            _playerLooking.SetColor(Color);
        }

        public override void OnStartLocalPlayer()
        {
            //base.OnStartLocalPlayer();

            Debug.Log($"start game player is local {Name}");
        }

        [Command]
        public void CmdSetName(string name)
        {

        }

        [Command]
        public void CmdSetColor(Color color)
        {

        }

        private void SetName(string oldName, string newName)
        {
            _playerLooking.SetName(newName);
        }

        private void SetColor(Color oldColor, Color newColor)
        {
            _playerLooking.SetColor(newColor);
        }
    }
}