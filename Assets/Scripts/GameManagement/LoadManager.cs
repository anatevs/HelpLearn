using GameCore;
using UnityEngine;

namespace GameManagement
{
    public sealed class LoadManager : MonoBehaviour
    {
        [SerializeField]
        private StartMenu _startMenu;

        [SerializeField]
        private SaveLoad_ShotsHits _saveLoadShots;

        [SerializeField]
        private SaveLoad_WeaponParams _saveLoadWeapon;

        private static LoadManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            _saveLoadShots.Load();

            if (_saveLoadShots.HasLoadedData())
            {
                _startMenu.ShowRecord(_saveLoadShots.Hits,
                    _saveLoadShots.Shots);
            }

            _saveLoadWeapon.Load();
        }
    }
}