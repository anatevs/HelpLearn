using UnityEngine;

namespace UI
{
    public class GameplayHud : MonoBehaviour,
        IGameplayHud
    {
        public ICollectBarView CollectBarView => _collectView;

        public IHealthView HealthView => _healthView;

        public ISwitchInputView SwitchInputView => _switchInputView;

        [SerializeField]
        private CollectBarView _collectView;

        [SerializeField]
        private HealthView _healthView;

        [SerializeField]
        private SwitchInputView _switchInputView;
    }
}