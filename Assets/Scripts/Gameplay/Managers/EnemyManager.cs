using Events;
using System;

namespace Gameplay
{
    public class EnemyManager : SpawnableTimerManager<Enemy>
    {
        protected override void Awake()
        {
            base.Awake();

            _namePrefix = "Enemy-";
        }

        protected override void MakeAtSpawn(Enemy enemy)
        {
            base.MakeAtSpawn(enemy);

            var e = new EnemyAppearedEvent(DateTime.Now,
                $"Enemy {enemy.Name} appeared at x:" +
                $" {enemy.transform.position.x}, z: {enemy.transform.position.z}",
                enemy.transform.position);

            GameSingleton.Instance.EventManager.TriggerEvent(e);
        }

        protected override void MakeAtUnspawn(Enemy enemy)
        {
            base.MakeAtUnspawn(enemy);

            enemy.ResetEnemy();
        }
    }
}