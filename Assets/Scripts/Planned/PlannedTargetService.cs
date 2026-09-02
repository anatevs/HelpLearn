using UnityEngine;

namespace Gameplay
{
    public class PlannedTargetService : SpawnService<Target>
    {
        public override Target Spawn(Transform spawnPoint, float speed)
        {
            var target = base.Spawn(spawnPoint, speed);

            target.SetParameters(speed, spawnPoint.forward);

            target.OnKilled += Unspawn;

            return target;
        }

        public override void Unspawn(Target target)
        {
            base.Unspawn(target);

            target.OnKilled -= Unspawn;

            target.SetParameters(0, Vector3.zero);
        }
    }
}