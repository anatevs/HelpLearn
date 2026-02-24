using Gameplay;
using UI;
using UnityEngine;

namespace GameManagement
{
    public class DDOLBootstrap : MonoBehaviour
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
        private string _countersService = "PlayerCounterService";

        [SerializeField]
        private string _itemService = "ItemService";

        private void Awake()
        {
            ProjectileSpawnService.CreateInstance(GetPathName(_projectilePrefabName));

            EnemySpawnService.CreateInstance(GetPathName(_enemiesPrefabName));

            GameStateService.CreateInstance(GetPathName(_gameStates));

            CanvasView.CreateInstance(GetPathName(_canvas));


            PlayerCountersService.CreateInstance(GetPathName(_countersService));

            PlayerCountersService.Instance.Init();


            ItemsService.CreateInstance(GetPathName(_itemService));
        }

        private string GetPathName(string prefabName)
        {
            return $"{_prefabsFolderPath}{prefabName}";
        }
    }
}