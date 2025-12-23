using GameCore;
using UnityEngine;

namespace GameManagement
{
    public sealed class LoadManager : MonoBehaviour
    {
        [SerializeField]
        private StartMenu _startMenu;

        [SerializeField]
        private SaveLoad_ShotsHits _saveLoad;

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

            if (_saveLoad.Load())
            {
                _startMenu.ShowRecord(_saveLoad.Hits,
                    _saveLoad.Shots);
            }
        }
    }
}