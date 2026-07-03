using Assets.Input;
using GameManagement;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class PlayerSceneDependencies : MonoBehaviour
    {
        public CameraFollower CameraFollower => _cameraFollower;

        public InputHandler InputHandler => _inputHandler;

        public HPView HPView => _hpView;

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
        private GameItemsConfig _gameItemsConfig;

        [SerializeField]
        private PickableItemsService _pickableItemsService;

        private HPPresenter _hpPresenter;

        private InventoriesPresenter _inventoriesPresenter;

        private void Awake()
        {
            _gameItemsConfig.Init();
            _pickableItemsService.Init(_gameItemsConfig);
        }

        private void OnDestroy()
        {
            _hpPresenter?.Dispose();
            _inventoriesPresenter?.Dispose();
        }

        public void ConstructPlayer(GamePlayer gamePlayer)
        {
            var isLocal = gamePlayer.Construct(_cameraFollower,
                _inputHandler, _weaponTracerShower,
                _gameItemsConfig, _pickableItemsService);

            if (isLocal)
            {
                _hpPresenter = new HPPresenter(_hpView, gamePlayer.Health);
                _playerLifeResetPresenter.Init(gamePlayer);
                _weaponPresenter.Init(gamePlayer.Weapon);
                _inventoriesPresenter = new InventoriesPresenter(_inventoriesView, gamePlayer, _gameItemsConfig);
            }
        }
    }
}