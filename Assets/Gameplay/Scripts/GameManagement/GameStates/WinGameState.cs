using UI;
using UnityEngine;

namespace GameManagement
{
    public class WinGameState : IGameState
    {
        public void Enter()
        {
            Time.timeScale = 0f;

            CanvasView.Instance.ShowWin();
        }
    }
}