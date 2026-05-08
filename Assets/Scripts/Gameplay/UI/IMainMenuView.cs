using System;
using UnityEngine;

namespace UI
{
    public interface IMainMenuView
    {
        public event Action OnStartClicked;
        public event Action OnExitClicked;

        public void Show();
        public void Hide();
    }
}