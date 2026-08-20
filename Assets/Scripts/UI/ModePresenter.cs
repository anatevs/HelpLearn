using GameManagement;
using System;

namespace UI
{
    public class ModePresenter :
        IDisposable
    {
        private readonly ModeView _modeView;

        private readonly PrewarmPresenter _prewarmPresenter;

        private readonly SpawnModeController _spawnModeController;

        public ModePresenter(ModeView modeView, SpawnModeController spawnModeController)
        {
            _modeView = modeView;
            _spawnModeController = spawnModeController;

            _modeView.SetModeName(_spawnModeController.Mode.ToString());

            _prewarmPresenter = new PrewarmPresenter(_modeView.PrewarmView);

            _spawnModeController.OnPrewarmPoolCreated += _prewarmPresenter.AddPrewarmPool;
        }

        public void Dispose()
        {
            if (_spawnModeController != null)
            {
                _spawnModeController.OnPrewarmPoolCreated -= _prewarmPresenter.AddPrewarmPool;
            }

            _prewarmPresenter?.Dispose();
        }
    }
}