using UnityEngine;

namespace Gameplay
{
    public class GameSingletonInstaller : MonoBehaviour
    {
        [SerializeField]
        private BulletManager _bulletManager;

        [SerializeField]
        private GameBorders _gameBorders;

        private void Awake()
        {
            InstallGameManagers();
        }

        private void InstallGameManagers()
        {
            GameSingleton.Instance.BulletManager = _bulletManager;

            GameSingleton.Instance.GameBorders = _gameBorders;
        }
    }
}