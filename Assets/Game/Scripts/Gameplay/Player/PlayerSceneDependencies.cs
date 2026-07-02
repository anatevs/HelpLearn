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

        private HPPresenter _hpPresenter;

        private void OnDestroy()
        {
            _hpPresenter?.Dispose();
        }

        public void ConstructPlayer(GamePlayer gamePlayer)
        {
            var isLocal = gamePlayer.ConstructLocal(_cameraFollower, _inputHandler, _weaponTracerShower);

            if (isLocal)
            {
                _hpPresenter = new HPPresenter(_hpView, gamePlayer.Health);
                _playerLifeResetPresenter.Init(gamePlayer);
                _weaponPresenter.Init(gamePlayer.Weapon);
            }
        }
    }
}