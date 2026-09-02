using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class PlannedProjectileService : SpawnService<Projectile>
    {
        public override Projectile Spawn(Transform spawnPoint, float speed)
        {
            var projectile = base.Spawn(spawnPoint, speed);

            projectile.transform.rotation = spawnPoint.rotation;

            //projectile.SetParameters(speed, lifetime, _damageMask);

            projectile.OnDestroyed += Unspawn;

            return projectile;
        }
    }
}