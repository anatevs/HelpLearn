using UnityEngine;

namespace Gameplay
{
    public abstract class ItemConfig : ScriptableObject
    {
        public abstract ItemType Type { get; }
        public string Name => _name;
        public float PickSqrDistance => _pickSqrDistance;
        public PickableItem PickablePrefab => _pickablePrefab;


        [SerializeField]
        private string _name;

        [SerializeField]
        private PickableItem _pickablePrefab;

        [SerializeField]
        private float _pickSqrDistance;
    }
}