using Events;

namespace Gameplay
{
    public sealed class GameSingleton
    {
        public static GameSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameSingleton();
                }

                return _instance;
            }
        }

        private static GameSingleton _instance;

        public EventManager EventManager => _eventManager;

        public GameManager GameManager => _gameManager;

        public BulletManager BulletManager { get => _bulletManager; set => _bulletManager = value; }

        public GameBorders GameBorders { get => _gameBorders; set => _gameBorders = value; }


        private EventManager _eventManager = new EventManager();

        private GameManager _gameManager = new GameManager();

        private BulletManager _bulletManager;

        private GameBorders _gameBorders;

        private GameSingleton()
        {
        }
    }
}