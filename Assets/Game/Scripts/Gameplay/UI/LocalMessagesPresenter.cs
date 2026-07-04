using GameManagement;
using System;

namespace UI
{
    public class LocalMessagesPresenter :
        IDisposable
    {
        private readonly LocalMessagesView _view;

        private readonly GamePlayer _player;

        public LocalMessagesPresenter(LocalMessagesView view, GamePlayer player)
        {
            _view = view;
            _player = player;

            _player.OnAbsentItemTried += HandleTryGetEmptyItem;
        }

        public void Dispose()
        {
            _player.OnAbsentItemTried -= HandleTryGetEmptyItem;
        }

        private void HandleTryGetEmptyItem(string itemName)
        {
            _view.ShowMessage($"You don't have any {itemName}");
        }
    }
}