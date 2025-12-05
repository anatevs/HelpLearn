namespace Task1_Items
{
    public class EnemiesController
    {
        public Enemy CurrentEnemy => _enemy;

        private Queue<Enemy> _enemies = new Queue<Enemy>();

        private Enemy _enemy = new Enemy("nullEnemy", 0, 0, 0);

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Enqueue(enemy);
        }

        public bool ChangeToNext()
        {
            if (!_enemies.TryDequeue(out var enemy))
            {
                Console.WriteLine();
                Console.WriteLine("no enemies left");
                _enemy = new();
                return false;
            }

            Console.WriteLine();
            Console.WriteLine($"Cnange to new enemy: {enemy.Name}");
            _enemy = enemy;
            return true;
        }
    }
}