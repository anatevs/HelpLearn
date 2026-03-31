using Gameplay;
using System;

namespace UI
{
    public sealed class WeaponPreseter : IDisposable
    {
        public event Action<string> OnClicked;

        private readonly WeaponView _view;
        private readonly Weapon _weapon;

        public WeaponPreseter(WeaponView view, Weapon weapon)
        {
            _view = view;
            _weapon = weapon;

            _view.OnClicked += HandleClick;

            _weapon.OnCapacityChanged += ChangeCapacity;
            _weapon.OnCooldownChanged += SetReadiness;
            _weapon.OnEmptied += SetInactive;
        }

        void IDisposable.Dispose()
        {
            _view.OnClicked -= HandleClick;

            _weapon.OnCapacityChanged -= ChangeCapacity;
            _weapon.OnCooldownChanged -= SetReadiness;
            _weapon.OnEmptied -= SetInactive;
        }

        public void Init(string name, int capacity)
        {
            _view.SetName(name);
            ChangeCapacity(capacity);
        }

        public void SetReadiness(float portion)
        {
            _view.SetReadiness(portion);
        }

        public void SetSelected()
        {
            _view.SetSelected();
        }

        private void ChangeCapacity(int newCapacity)
        {
            _view.SetCapacity(newCapacity.ToString());
        }

        private void HandleClick()
        {
            OnClicked?.Invoke(_weapon.Name);
        }

        private void SetInactive()
        {
            _view.SetInactive();
        }
    }
}