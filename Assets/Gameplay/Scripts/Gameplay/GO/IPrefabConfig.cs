using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay
{
    public interface IPrefabConfig<T> where T: MonoBehaviour
    {
        public T Prefab { get; set; }
        public AssetReference PrefabReference { get; }
        public string Name { get; }
    }
}