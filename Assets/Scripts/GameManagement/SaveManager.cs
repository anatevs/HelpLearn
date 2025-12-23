using GameCore;
using UnityEngine;

namespace GameManagement
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerShooting _playerShooting;

        [SerializeField]
        private SaveLoad_ShotsHits _saveLoad;

        private void OnEnable()
        {
            _playerShooting.Shooted += OnShotted;
            _playerShooting.Hitted += OnHitted;
        }

        public void OnDisable()
        {
            _playerShooting.Shooted -= OnShotted;
            _playerShooting.Hitted -= OnHitted;

            _saveLoad.Save();
        }

        private void OnShotted(int shoots)
        {
            _saveLoad.Shots = shoots;
        }

        private void OnHitted(int hits)
        {
            _saveLoad.Hits = hits;
        }
    }
}