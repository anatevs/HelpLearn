using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Items
{
    public class InitEnemy
    {
        public Enemy Enemy => _enemy;
        public EnemiesController EnemiesController => _enemiesController;

        private Enemy _enemy;
        private EnemiesController _enemiesController;

        public InitEnemy()
        {
            var enemies = new Queue<Enemy>();

            _enemiesController = new EnemiesController();

            _enemiesController.AddEnemy(new Enemy("Enemy-dmg1-rw5", 10, 1, 5));
            _enemiesController.AddEnemy(new Enemy("Enemy-dmg2-rw8", 20, 2, 8));
            _enemiesController.AddEnemy(new Enemy("Enemy-dmg4-rw15", 10, 4, 15));

            if (!_enemiesController.ChangeToNext())
            {
                Console.WriteLine("no enemies in enemies controller!!");
            }

            _enemy = _enemiesController.CurrentEnemy;
        }
    }
}