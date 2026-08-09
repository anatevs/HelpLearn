using Mirror;
using TMPro;
using GameManagement;

namespace UI
{
    public class GameInfoPanel : NetworkBehaviour
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
                var initializer = lobbyManager.GameInitializer;

                initializer.RegisterInfoPanel(this);
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
            if (isServer)
            {
                ShowLogText(logText);
            }

            if (_logText == logText)
            {
                _logText = $"{logText} ";
                return;
            }

            _logText = logText;
        }

        public void SetPlayersCount(int count)
        {
            _playersCount = count;

            if (isServer)
            {
                ShowPlayersCount(_playersCount);
            }
        }

        private void HookAddLog(string oldAdd, string newAdd)
        {
            if (isServer)
            {
                return;
            }

            ShowLogText(newAdd);
        }

        private void HookPlayersCount(int oldCount, int newCount)
        {
            if (!isServer)
            {
                ShowPlayersCount(newCount);
            }
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