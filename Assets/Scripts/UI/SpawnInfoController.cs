using GameTest;
using System;

namespace UI
{
    public class SpawnInfoController :
        IDisposable
    {
        private readonly SpawnInfoView _view;
        private readonly SpawnCounter _spawnCounter;

        public SpawnInfoController(SpawnInfoView view,
            SpawnCounter spawnCounter)
        {
            _view = view;
            _spawnCounter = spawnCounter;

            SetValues(_spawnCounter.Data);

            _spawnCounter.OnDataChanged += SetValues;
        }

        public void Dispose()
        {
            if (_view != null)
            {
                _spawnCounter.OnDataChanged -= SetValues;
            }
        }

        private void SetValues(SpawnCountData data)
        {
            _view.SetTotalAndCurrent(data.TotalCount.ToString(),
                data.ActiveCount.ToString());
        }
    }
}