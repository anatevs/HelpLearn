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

        public EventManager EventManager => _eventManager;

        private static GameSingleton _instance;

        private EventManager _eventManager;

        private GameSingleton()
        {
            _eventManager = new EventManager();
        }
    }
}