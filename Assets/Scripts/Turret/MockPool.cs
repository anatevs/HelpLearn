using UnityEngine;

namespace Gameplay
{
    public class MockPool<T> : IPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;

        public MockPool(T prefab)
        {
            _prefab = prefab;
        }

        public T Get()
        {
            return UnityEngine.Object.Instantiate(_prefab);
        }

        public void Release(T item)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }
}