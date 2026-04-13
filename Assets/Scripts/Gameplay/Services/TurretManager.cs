using UnityEngine;

namespace Gameplay
{
    public sealed class TurretManager : MonoBehaviour
    {
        private Turret[] _turrets;

        private Player _player;
        private ProjectileSpawnService _projectileSpawn;

        public void Construct(Player player, ProjectileSpawnService projectileSpawn)
        {
            _player = player;
            _projectileSpawn = projectileSpawn;

            _turrets = GetComponentsInChildren<Turret>();

            foreach (var turret in _turrets)
            {
                turret.Construct(_projectileSpawn, _player.transform);
                turret.Init();
            }
        }

        public void ResetLevel()
        {
            foreach (var turret in _turrets)
            {
                turret.ResetLevel();
            }
        }
    }
}