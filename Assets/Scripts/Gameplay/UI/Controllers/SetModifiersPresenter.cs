using Gameplay;
using System;

namespace UI
{
    public sealed class SetModifiersPresenter :
        IDisposable
    {
        private readonly ISetModifiersView _view;

        private readonly GameplayController _gameplayController;

        private readonly GameModifiersConfig _gameplayModifiersConfig;

        public SetModifiersPresenter(ISetModifiersView view,
            GameplayController gameplayController,
            GameModifiersConfig gameplayModifiers)
        {
            _view = view;
            _gameplayController = gameplayController;
            _gameplayModifiersConfig = gameplayModifiers;

            _view.OnApplyClicked += ApplyRandomModifier;
            _view.OnCancelClicked += CancelRandomModifier;
        }

        public void Dispose()
        {
            _view.OnApplyClicked -= ApplyRandomModifier;
            _view.OnCancelClicked -= CancelRandomModifier;
        }

        private void ApplyRandomModifier()
        {
            var configs = _gameplayModifiersConfig.Configs;
            var index = UnityEngine.Random.Range(0, configs.Length);

            _gameplayController
                .AddModifier(configs[index]);
        }

        private void CancelRandomModifier()
        {
            var modifiers = _gameplayController.Modifiers;

            if (modifiers.Count > 0)
            {
                var index = UnityEngine.Random.Range(0, modifiers.Count);

                _gameplayController
                    .CancelModifier(modifiers[index]);
            }
        }
    }
}