using System;
using UnityEngine;

namespace Network.UI
{
    public interface ILobbyPlayerSettingsView
    {
        public event Action<string> OnNameSet;

        public event Action<Color> OnColorSet;

        public void Show(string currentName, Color currentColor);

        public void Hide();
    }
}