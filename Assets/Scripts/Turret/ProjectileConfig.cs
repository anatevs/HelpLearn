using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ProjectileConfig",
        menuName = "Configs/Projectile")]
    public class ProjectileConfig : ScriptableObject
    {
        public Projectile Prefab => _prefab;

        [SerializeField]
        private Projectile _prefab;
    }
}