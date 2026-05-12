using System.Collections.Generic;

namespace GameManagement
{
    public class GameResetService
    {
        private readonly List<IResetable> _resetables = new();

        public void AddResetable(IResetable resetable)
        {
            _resetables.Add(resetable);
        }

        public void ResetGame()
        {
            for (int i = 0; i < _resetables.Count; i++)
            {
                _resetables[i].ResetLevel();
            }
        }
    }
}