using Assets.Input;
using GameManagement;
using Mirror;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class PlayerSceneDependencies : MonoBehaviour
    {
        [SerializeField]
        private CameraFollower _cameraFollower;

        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private HPView _hpView;

        [SerializeField]
        private WeaponTracerShower _weaponTracerShower;

        [SerializeField]
        private PlayerLifeResetPresenter _playerLifeResetPresenter;

        [SerializeField]
        private WeaponPresenter _weaponPresenter;

        [SerializeField]
        private InventoriesView _inventoriesView;

        [SerializeField]
        private PickItemsSpawnConfig _pickItemsSpawnConfig;

        [SerializeField]
        private PickableItemsService _pickableItemsService;

        [SerializeField]
        private GrenadesService _grenadesService;

        [SerializeField]
        private LocalMessagesView _localMessagesView;

        [Header("Match")]
        [SerializeField]
        private MatchTimer _matchTimer;

        [SerializeField]
        private MatchTimerView _matchTimerView;

        private HPPresenter _hpPresenter;

        private InventoriesPresenter _inventoriesPresenter;

        private GameplayInfoPresenter _gameplayInfoPresenter = new();

        private LocalMessagesPresenter _localMessagesPresenter;

        private MatchConfig _matchConfig;

        private MatchTimerPresenter _timerPresenter;

        //private LeaderboardStorage _leaderboardStorage;

        private void Awake()
        {
            ShowGameplayUI(false);

            _pickItemsSpawnConfig.Init();
            _pickableItemsService.Init(_pickItemsSpawnConfig);
            _grenadesService.Init(_pickItemsSpawnConfig);

            if (NetworkManager.singleton is LobbyManager lobbyManager)
            {
                _matchConfig = lobbyManager.MatchConfig;
                //_leaderboardStorage = lobbyManager.LeaderboardStorage;
            }

            _matchTimer.Init(_matchConfig.MatchTime);

            _timerPresenter = new MatchTimerPresenter(_matchTimerView, _matchTimer);
        }

        private void OnDestroy()
        {
            _hpPresenter?.Dispose();
            _inventoriesPresenter?.Dispose();
            _gameplayInfoPresenter?.Dispose();
            _timerPresenter?.Dispose();
        }

        public void ConstructPlayer(GamePlayer gamePlayer)
        {
            var isLocal = gamePlayer.Construct(_cameraFollower,
                _inputHandler, _weaponTracerShower,
                _pickItemsSpawnConfig, _grenadesService);

            _gameplayInfoPresenter.AddPlayer(gamePlayer);

            if (isLocal)
            {
                ShowGameplayUI(true);

                _hpPresenter = new HPPresenter(_hpView, gamePlayer.Health);
                _playerLifeResetPresenter.Init(gamePlayer);
                _weaponPresenter.Init(gamePlayer.Weapon);
                _inventoriesPresenter = new InventoriesPresenter(_inventoriesView, gamePlayer,
                    _pickItemsSpawnConfig.ItemsSpawnData);
                _localMessagesPresenter = new LocalMessagesPresenter(_localMessagesView, gamePlayer);
            }
        }

        public void ShowGameplayUI(bool isShow)
        {
            _hpView.gameObject.SetActive(isShow);

            _weaponPresenter.ShowView(isShow);

            _localMessagesView.gameObject.SetActive(isShow);
        }
    }
}