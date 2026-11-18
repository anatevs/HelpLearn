using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ItemConfig",
        menuName = "Configs/Item")]
    public sealed class ItemConfig : ScriptableObject, IPrefabConfig<Item>
    {
        public Item Prefab
        {
            get => _prefab;
            set { _prefab = value; }
        }

        public string Name => _name;

        public AssetReferenceGameObject PrefabReference => _prefabReference;

        [SerializeField]
        private AssetReferenceGameObject _prefabReference;

        [SerializeField]
        private string _name = "";

        private Item _prefab;
    }
}