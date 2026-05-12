using Gameplay;
using System;

namespace UI
{
    public sealed class CollectBarController :
        IDisposable
    {
        private readonly ICollectService _collectService;
        private readonly ICollectBarView _view;

        public CollectBarController(ICollectService collectService, ICollectBarView view)
        {
            _collectService = collectService;
            _view = view;

            foreach (var name in _collectService.Names)
            {
                _view.AddView(_collectService.GetItemConfig(name), _collectService.GetAmount(name));
            }

            _collectService.OnNewAdded += _view.AddView;
            _collectService.OnChanged += _view.SetAmount;
            _collectService.OnRemoved += _view.RemoveView;
        }

        public void Dispose()
        {
            _collectService.OnNewAdded -= _view.AddView;
            _collectService.OnChanged -= _view.SetAmount;
            _collectService.OnRemoved -= _view.RemoveView;
        }
    }
}