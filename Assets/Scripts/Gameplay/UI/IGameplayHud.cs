namespace UI
{
    public interface IGameplayHud
    {
        public ICollectBarView CollectBarView { get; }

        public IHealthView HealthView { get; }

        public ISwitchInputView SwitchInputView { get; }
    }
}