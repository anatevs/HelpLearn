using Gameplay;
using GameTest;
using System;
using System.Collections.Generic;

namespace UI
{
    public class ObjectSpawnInfoPresenter :
        IDisposable
    {
        private readonly SpawnInfoView _view;
        private readonly SpawnCounter _spawnCounter;
        private readonly List<IInfoPool> _infoPools;

        private int _poolSize;
        private int _freeCount;
        private int _repeatUsing;

        public ObjectSpawnInfoPresenter(SpawnInfoView view,
            SpawnCounter spawnCounter, List<IInfoPool> infoPools)
        {
            _view = view;
            _spawnCounter = spawnCounter;
            _infoPools = infoPools;

            SetValues(_spawnCounter.Data);

            _spawnCounter.OnDataChanged += SetValues;


            var isPoolInfo = _infoPools != null;

            _view.ShowPoolInfo(isPoolInfo);

            if (isPoolInfo)
            {
                foreach (var pool in _infoPools)
                {
                    var (Size, Free, Repeat) = pool.GetInfo();

                    _poolSize += Size;
                    _freeCount += Free;
                    _repeatUsing += Repeat;

                    pool.OnPoolSizeChanged += SetPoolSizeChange;
                    pool.OnCurrentFreeChanged += SetFreeCountChange;
                    pool.OnRepeatUsingChanged += SetRepeatUsingChange;
                }

                SetPoolSizeChange(0);
                SetFreeCountChange(0);
                SetRepeatUsingChange(0);
            }
        }

        public void Dispose()
        {
            if (_view != null)
            {
                _spawnCounter.OnDataChanged -= SetValues;

                if (_infoPools != null)
                {
                    foreach (var pool in _infoPools)
                    {
                        pool.OnPoolSizeChanged -= SetPoolSizeChange;
                        pool.OnCurrentFreeChanged -= SetFreeCountChange;
                        pool.OnRepeatUsingChanged -= SetRepeatUsingChange;
                    }
                }
            }
        }

        private void SetValues(SpawnCountData data)
        {
            _view.SetTotalAndCurrent(data.TotalCount.ToString(),
                data.ActiveCount.ToString());
        }

        private void SetPoolSizeChange(int poolSizeChange)
        {
            _poolSize += poolSizeChange;
            _view.SetPoolSize(_poolSize.ToString());
        }

        private void SetFreeCountChange(int freeCountChange)
        {
            _freeCount += freeCountChange;
            _view.SetFreeCount(_freeCount.ToString());
        }

        private void SetRepeatUsingChange(int repeatUsingChange)
        {
            _repeatUsing += repeatUsingChange;
            _view.SetRepeatUsing(_repeatUsing.ToString());
        }
    }
}