using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameManagement
{
    public class LobbyPlayersManager
    {
        public int Count => _lobbyPlayers.Count;

        public event Action<int, string> OnNameChanged;
        public event Action<int, Color> OnColorChanged;
        public event Action<int, string, Color, bool, bool> OnSlotUpdated;
        public event Action<int, bool> OnReadyChanged;
        public event Action<int, bool> OnPlayerAdded;
        public event Action<int> OnPlayerRemoved;

        private readonly MultiplayerSettingsConfig _settingsConfig;

        private readonly List<LobbyPlayer> _lobbyPlayers = new();

        public LobbyPlayersManager(MultiplayerSettingsConfig settingsConfig)
        {
            _settingsConfig = settingsConfig;
        }

        public LobbyPlayer GetPlayer(int id)
        {
            return _lobbyPlayers[id];
        }

        public LobbyPlayer GetPlayer(string name)
        {
            return _lobbyPlayers.Find(x => x.Name == name);
        }

        public void AddPlayer(LobbyPlayer lobbyPlayer, NetworkConnectionToClient conn)
        {
            lobbyPlayer.PlayerID = _lobbyPlayers.Count;
            lobbyPlayer.Init(GetDefaultName(lobbyPlayer.PlayerID));

            lobbyPlayer.OnNameChangeRequested += HandleChangeNameRequest;
            lobbyPlayer.OnColorChanged += ChangeColor;
            lobbyPlayer.OnReadyChanged += ChangeReady;

            NetworkServer.AddPlayerForConnection(conn, lobbyPlayer.gameObject);

            _lobbyPlayers.Add(lobbyPlayer);

            OnPlayerAdded?.Invoke(lobbyPlayer.PlayerID, lobbyPlayer.isOwned);

            foreach (var player in _lobbyPlayers)
            {
                OnSlotUpdated?.Invoke(player.PlayerID, player.Name, player.Color, player.ReadyToBegin, player.isOwned);
            }
        }

        public void RemovePlayer(LobbyPlayer lobbyPlayer)
        {
            ChangeReady(lobbyPlayer.PlayerID, false);

            lobbyPlayer.OnNameChangeRequested -= HandleChangeNameRequest;
            lobbyPlayer.OnColorChanged -= ChangeColor;
            lobbyPlayer.OnReadyChanged -= ChangeReady;

            _lobbyPlayers.Remove(lobbyPlayer);

            for (int i = lobbyPlayer.PlayerID; i < _lobbyPlayers.Count; i++)
            {
                var newName = _lobbyPlayers[i].Name;

                if (_lobbyPlayers[i].Name == GetDefaultName(i + 1))
                {
                    newName = GetDefaultName(i);
                    _lobbyPlayers[i].Name = newName;
                }

                _lobbyPlayers[i].PlayerID = i;

                OnSlotUpdated?.Invoke(_lobbyPlayers[i].PlayerID, _lobbyPlayers[i].Name, _lobbyPlayers[i].Color, _lobbyPlayers[i].ReadyToBegin, _lobbyPlayers[i].isOwned);
            }

            var emptyIndex = _lobbyPlayers.Count;

            OnPlayerRemoved?.Invoke(emptyIndex);
        }

        public void Clear()
        {
            _lobbyPlayers.Clear();
        }




        #region Player data changing

        public void HandleChangeNameRequest(int index, string newName)
        {
            if (!CanSetName(index, newName))
            {
                return;
            }

            ChangeName(index, newName);
        }

        public void ChangeName(int index, string newName)
        {
            _lobbyPlayers[index].Name = newName;
            OnNameChanged?.Invoke(index, newName);
        }

        public void ChangeColor(int index, Color color)
        {
            OnColorChanged?.Invoke(index, color);
        }

        public void ChangeReady(int index, bool isReady)
        {
            OnReadyChanged?.Invoke(index, isReady);

            //ReadyStatusChanged();
        }

        private string GetDefaultName(int index)
        {
            return $"{_settingsConfig.DefaultNamePrefix}{index}";
        }

        private bool CanSetName(int index, string newName)
        {
            bool isOtherDefault = (newName.StartsWith(_settingsConfig.DefaultNamePrefix) &&
                int.TryParse(newName[_settingsConfig.DefaultNamePrefix.Length..], out int number) &&
                number >= 0 &&
                number < _settingsConfig.MaxPlayers &&
                number != index);

            if (newName.Length < _settingsConfig.NameLengthRange[0]
                || newName.Length > _settingsConfig.NameLengthRange[1]
                || (_lobbyPlayers.Any(x => x.Name == newName))
                || isOtherDefault)
            {
                return false;
            }

            return true;
        }

        #endregion


        //public virtual void ReadyStatusChanged()
        //{
        //    int currentPlayers = 0;
        //    int readyPlayers = 0;

        //    foreach (LobbyPlayer player in _lobbyPlayers)
        //    {
        //        if (player != null)
        //        {
        //            currentPlayers++;
        //            if (player.ReadyToBegin)
        //                readyPlayers++;
        //        }
        //    }

        //    //if (currentPlayers == readyPlayers)
        //    //    CheckReadyToBegin();
        //    //else
        //    //    ChangeEnoughReady(false);
        //}
    }
}