using GameManagement;
using Mirror;
using TMPro;

namespace UI
{
    public class GameInfoPanelPresenter : NetworkBehaviour
    {
        private GameLogView _logView;

        private TMP_Text _playersCountText;

        [SyncVar(hook = nameof(HookAddLog))]
        private string _logText;

        [SyncVar(hook = nameof(HookPlayersCount))]
        private int _playersCount;

        private int _emptyCount = 0;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (NetworkManager.singleton is LobbyManager lobbyManager)
            {
                lobbyManager.RegisterInfoPanel(this);
            }
        }

        public void Init(GameLogView logView, TMP_Text playersCountText)
        {
            _logView = logView;
            _playersCountText = playersCountText;

            ClearViews();
        }

        private void OnDisable()
        {
            if (_logView != null && _playersCountText != null)
            {
                ClearViews();
            }
        }
        public void AddLog(string logText)
        {
            _logText = logText;
        }

        public void SetPlayersCount(int count)
        {
            _playersCount = count;
        }

        private void HookAddLog(string oldAdd, string newAdd)
        {
            ShowLogText(newAdd);
        }

        private void HookPlayersCount(int oldCount, int newCount)
        {
            ShowPlayersCount(newCount);
        }

        private void ShowLogText(string text)
        {
            _logView.AddLog(text);
        }

        private void ShowPlayersCount(int count)
        {
            _playersCountText.text = count.ToString();
        }

        private void ClearViews()
        {
            ShowPlayersCount(_emptyCount);

            _logView.ClearLogView();
        }
    }
}