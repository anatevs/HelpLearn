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

            _player.OnInventoryNamedUpdeted += UpdateValue;

            foreach (var data in itemsData)
            {
                _view.AddNewView(data.Config.Name, _initValue.ToString());
            }
        }

        public void Dispose()
        {
            _player.OnInventoryNamedUpdeted -= UpdateValue;
        }

        private void UpdateValue(string itemName, int value)
        {
            _view.SetValue(itemName, value.ToString());
        }
    }
}