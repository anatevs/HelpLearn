using UnityEngine;

namespace Gameplay
{
    public abstract class ItemConfig : ScriptableObject
    {
        public abstract ItemType Type { get; }
        public string Name => _name;
        public PickableItem PickablePrefab => _pickablePrefab;
        public WaitForSeconds UseWait => new WaitForSeconds(_useDelay);

        [SerializeField]
        private string _name;

        [SerializeField]
        private PickableItem _pickablePrefab;

        [SerializeField]
        private float _useDelay;
    }
}