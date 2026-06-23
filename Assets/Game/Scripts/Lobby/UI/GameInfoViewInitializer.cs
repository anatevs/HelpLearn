using TMPro;
using UnityEngine;

namespace UI
{
    public class GameInfoViewInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameLogView _logView;

        [SerializeField]
        private TMP_Text _playersCountText;

        public void SetupInfoPanel(GameInfoPanelPresenter panel)
        {
            panel.Init(_logView, _playersCountText);
        }
    }
}