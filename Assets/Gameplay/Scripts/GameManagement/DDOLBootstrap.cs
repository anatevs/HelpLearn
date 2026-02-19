using Gameplay;
using UnityEngine;

namespace GameManagement
{
    public class DDOLBootstrap : MonoBehaviour
    {
        [SerializeField]
        private string _projectileServicePath = "GameServices/Projectiles";

        private void Awake()
        {
            ProjectileSpawnService.CreateInstance(_projectileServicePath);
        }
    }
}