using Gameplay;
using System;
using System.Collections.Generic;

namespace UI
{
    public class PrewarmPresenter :
        IDisposable
    {
        private readonly PrewarmView _view;

        private readonly List<IPrewarmPool> _prewarmPools = new();

        public PrewarmPresenter(PrewarmView view)
        {
            _view = view;

            _view.OnClicked += HandlePrewarm;
        }

        public void Dispose()
        {
            if (_view != null)
            {
                _view.OnClicked -= HandlePrewarm;
            }
        }

        public void AddPrewarmPool(IPrewarmPool prewarmPool)
        {
            _prewarmPools.Add(prewarmPool);
            if (prewarmPool != null)
            {
                Show(true);
            }
        }

        private void Show(bool isShow)
        {
            _view.Show(isShow);
        }

        private void HandlePrewarm()
        {
            foreach (var pool in _prewarmPools)
            {
                pool.PopulatePool();
            }

            _view.SetInactive();
        }
    }
}