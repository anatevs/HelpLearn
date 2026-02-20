using Gameplay;
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

        private void Awake()
        {
            ProjectileSpawnService.CreateInstance($"{_prefabsFolderPath}{_projectilePrefabName}");

            EnemySpawnService.CreateInstance($"{_prefabsFolderPath}{_enemiesPrefabName}");
        }
    }
}