using Gameplay;
using Input;
using System;

namespace UI
{
    public sealed class GameplayHudController :
        IDisposable
    {
        private readonly IGameplayHud _gameplayHud;

        private readonly CollectBarController _collectBarController;

        private readonly InputSwitchPresenter _inputSwitchPresenter;

        private readonly IHealth _health;

        public GameplayHudController(IGameplayHud gameplayHud,
            ICollectService collectService,
            IHealth health,
            IInputSwitchService inputSwitchService)
        {
            _gameplayHud = gameplayHud;

            _health = health;
            ChangeHP(_health.HP);
            _health.OnHPChanged += ChangeHP;

            _collectBarController = new CollectBarController(collectService, _gameplayHud.CollectBarView);
            _inputSwitchPresenter = new InputSwitchPresenter(inputSwitchService, _gameplayHud.SwitchInputView);
        }

        public void Dispose()
        {
            _collectBarController.Dispose();

            _health.OnHPChanged -= ChangeHP;

            _inputSwitchPresenter.Dispose();
        }

        private void ChangeHP(int newHP)
        {
            _gameplayHud.HealthView.SetHP(newHP.ToString());
        }
    }
}