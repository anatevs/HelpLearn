using Gameplay;
using System;

namespace UI
{
    public sealed class ModifiersPresenter :
        IDisposable
    {
        private readonly IModifiersInfoView _view;

        private readonly GameplayController _gameplayController;

        public ModifiersPresenter(IModifiersInfoView view,
            GameplayController gameplayController)
        {
            _view = view;
            _gameplayController = gameplayController;

            _gameplayController.OnModifierAdded += _view.AddModifierName;
            _gameplayController.OnModifierRemoved += _view.RemoveModifierName;

            var names = _gameplayController.Names;

            for (int i = 0; i < names.Length; i++)
            {
                _view.AddModifierName(names[i]);
            }
        }

        public void Dispose()
        {
            _gameplayController.OnModifierAdded -= _view.AddModifierName;
            _gameplayController.OnModifierRemoved -= _view.RemoveModifierName;
        }
    }
}