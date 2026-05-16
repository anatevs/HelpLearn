using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "LogMessagesConfig",
        menuName = "Configs/LogMessages")]
    public sealed class LogMessagesConfig : ScriptableObject
    {
        public string HPChanged => _hpChanged;
        public string ItemPicked => _itemPicked;
        public string ItemSpawed => _itemSpawned;
        public string PlayerKilled => _playerKilled;

        public string InputSwitched => _inputSwitched;

        public string ModifierApply => _modifierApply;
        public string ModifierCancel => _modifierCancel;

        [SerializeField]
        private string _hpChanged;

        [SerializeField]
        private string _itemSpawned;

        [SerializeField]
        private string _itemPicked;

        [SerializeField]
        private string _playerKilled;

        [SerializeField]
        private string _inputSwitched;

        [SerializeField]
        private string _modifierApply;

        [SerializeField]
        private string _modifierCancel;
    }
}