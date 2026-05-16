using Gameplay;
using System;

namespace UI
{
    public sealed class ModifiersInfoPresenter :
        IDisposable
    {
        private readonly IModifiersInfoView _infoView;

        private readonly ILoggerService _loggerService;

        private readonly GameplayController _gameplayController;

        public ModifiersInfoPresenter(IModifiersInfoView view,
            GameplayController gameplayController,
            ILoggerService loggerService)
        {
            _infoView = view;
            _gameplayController = gameplayController;
            _loggerService = loggerService;

            _gameplayController.OnModifierAdded += HandleAdd;
            _gameplayController.OnModifierRemoved += HandleRemove;
            _gameplayController.OnModifierCanceled += HandleCancel;

            var modifiers = _gameplayController.Modifiers;

            for (int i = 0; i < modifiers.Count; i++)
            {
                _infoView.AddModifierName(modifiers[i].Name);
            }
        }

        public void Dispose()
        {
            _gameplayController.OnModifierAdded -= HandleAdd;
            _gameplayController.OnModifierRemoved -= HandleRemove;
            _gameplayController.OnModifierCanceled -= HandleCancel;
        }

        private void HandleAdd(IGameModifier modifier)
        {
            _infoView.AddModifierName(modifier.Name);
            _loggerService.LogModifierApply(modifier.Name);
        }

        private void HandleRemove(IGameModifier modifier)
        {
            _infoView.RemoveModifierName(modifier.Name);
        }

        private void HandleCancel(IGameModifier modifier)
        {
            _loggerService.LogModifierCancel(modifier.Name);
        }
    }
}