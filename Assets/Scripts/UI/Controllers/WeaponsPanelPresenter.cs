using Gameplay;
using System;
using System.Collections.Generic;

namespace UI
{
    public sealed class WeaponsPanelPresenter : IDisposable
    {
        private readonly WeaponsMenu _weaponsMenu;

        private readonly WeaponStorage _weaponStorage;

        private readonly List<WeaponPreseter> _weaponPreseters = new();

        public WeaponsPanelPresenter(WeaponsMenu weaponsMenu,
            WeaponStorage weaponStorage)
        {
            _weaponsMenu = weaponsMenu;
            _weaponStorage = weaponStorage;

            _weaponStorage.OnWeaponAdded += CreateView;
        }

        public void ResetLevel()
        {
            _weaponPreseters[_weaponStorage.FirstActiveIndex]
                .SetSelected();
        }

        void IDisposable.Dispose()
        {
            _weaponStorage.OnWeaponAdded -= CreateView;

            foreach (var presenter in _weaponPreseters)
            {
                presenter.OnClicked -= _weaponStorage.ChangeWeapon;
            }
        }

        private void CreateView(Weapon weapon, int initCapacity)
        {
            var view = _weaponsMenu.AddView();

            var presenter = new WeaponPreseter(view, weapon);

            presenter.Init(weapon.Name, initCapacity);

            _weaponPreseters.Add(presenter);

            presenter.OnClicked += _weaponStorage.ChangeWeapon;
        }
    }
}