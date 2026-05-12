using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public sealed class GameExit : IGameExit
    {
        private readonly List<IDisposable> _disposables = new();

        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void QuitGame()
        {
            for (int i = 0; i < _disposables.Count; i++)
            {
                _disposables[i].Dispose();
            }

            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}