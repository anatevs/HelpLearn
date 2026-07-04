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

        public InventoriesPresenter(InventoriesView view, GamePlayer player, ItemSpawnData[] itemsData)
        {
            _player = player;
            _view = view;

            _player.OnInventoryUpdated += UpdateValue;

            foreach (var data in itemsData)
            {
                _view.AddNewView(data.Config.Type, _initValue.ToString());
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