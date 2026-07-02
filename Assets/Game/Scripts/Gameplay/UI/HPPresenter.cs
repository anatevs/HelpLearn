using Gameplay;
using System;
using UnityEngine;

namespace UI
{
    public class HPPresenter : IDisposable
    {
        private readonly HPView _view;

        private readonly Health _health;

        public HPPresenter(HPView view, Health health)
        {
            _view = view;
            _health = health;

            _health.OnHealthChange += HandleHPChange;

            HandleHPChange(0, _health.HP);
        }

        public void Dispose()
        {
            if (_health != null)
            {
                _health.OnHealthChange -= HandleHPChange;
            }
        }

        private void HandleHPChange(int _, int newHP)
        {
            _view.SetHP(newHP.ToString());
        }
    }
}