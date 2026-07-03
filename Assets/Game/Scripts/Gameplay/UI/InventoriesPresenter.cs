using GameManagement;
using Gameplay;
using System;

namespace UI
{
    public class InventoriesPresenter :
        IDisposable
    {
        private readonly InventoriesView _view;

        private readonly GamePlayer _player;

        private int _initValue = 0;

        public InventoriesPresenter(InventoriesView view, GamePlayer player, GameItemsConfig itemsConfig)
        {
            _player = player;
            _view = view;

            _player.OnInventoryUpdated += UpdateValue;

            foreach (var config in itemsConfig.Configs)
            {
                _view.AddNewView(config.Type, _initValue.ToString());
            }
        }

        public void Dispose()
        {
            _player.OnInventoryUpdated -= UpdateValue;
        }

        private void UpdateValue(ItemType type, int value)
        {
            _view.SetValue(type, value.ToString());
        }
    }
}