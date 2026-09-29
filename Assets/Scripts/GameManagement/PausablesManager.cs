using System.Collections.Generic;

namespace GameManagement
{
    public class PausablesManager : IPausable
    {
        private readonly List<IPausable> _pausables = new();

        public void Add(IPausable pausable)
        {
            _pausables.Add(pausable);
        }

        public void Remove(IPausable pausable)
        {
            _pausables.Remove(pausable);
        }

        public void Pause()
        {
            foreach (var pausable in _pausables)
            {
                pausable.Pause();
            }
        }

        public void Resume()
        {
            foreach (var pausable in _pausables)
            {
                pausable.Resume();
            }
        }
    }
}