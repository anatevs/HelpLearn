using Gameplay;
using UI;
using UnityEngine;

namespace GameManagement
{
    public sealed class DDOLBootstrap : MonoBehaviour
    {
        [SerializeField]
        private string _prefabsFolderPath = "GameServices/";

        [SerializeField]
        private string _projectilePrefabName = "Projectiles";

        [SerializeField]
        private string _enemiesPrefabName = "Enemies";

        [SerializeField]
        private string _gameStates = "GameStates";

        [SerializeField]
        private string _canvas = "Canvas";

        [SerializeField]
        private string _itemService = "ItemService";

        [SerializeField]
        private string _countersController = "PlayerCountersController";

        [SerializeField]
        private string _saveService = "SaveService";

        [SerializeField]
        private string _soundPlayer = "SoundPlayer";

        private void Awake()
        {
            ProjectileSpawnService.CreateInstance(GetPathName(_projectilePrefabName));

            EnemySpawnService.CreateInstance(GetPathName(_enemiesPrefabName));

            GameStateService.CreateInstance(GetPathName(_gameStates));

            CanvasView.CreateInstance(GetPathName(_canvas));

            ItemsService.CreateInstance(GetPathName(_itemService));

            PlayerCountersController.CreateInstance(GetPathName(_countersController));

            SaveService.CreateInstance(GetPathName(_saveService));

            SoundPlayer.CreateInstance(GetPathName(_soundPlayer));
        }

        private string GetPathName(string prefabName)
        {
            return $"{_prefabsFolderPath}{prefabName}";
        }
    }
}