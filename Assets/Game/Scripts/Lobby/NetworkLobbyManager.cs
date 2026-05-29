using Mirror;
using Network.UI;
using UnityEngine;

namespace GameManagement
{
    public class NetworkLobbyManager : NetworkRoomManager
    {
        private bool _showStartButton;

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

        //public override void OnRoomClientSceneChanged()
        //{
        //    base.OnRoomClientSceneChanged();

        //    if (Utils.IsSceneActive(GameplayScene))
        //    {
        //        foreach (var roomPlayer in roomSlots)
        //        {
        //            if (roomPlayer is NetworkLobbyPlayer lobbyPlayer)
        //            {
        //                lobbyPlayer.DisableLobbyPlayer();
        //            }
        //        }
        //    }
        //}

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
            
            //PlayerScore playerScore = gamePlayer.GetComponent<PlayerScore>();
            //playerScore.index = roomPlayer.GetComponent<NetworkRoomPlayer>().index;
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