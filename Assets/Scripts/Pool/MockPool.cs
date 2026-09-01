using UnityEngine;

namespace Gameplay
{
    public class MockPool<T> : IPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;

        private Transform _parentTransform;

        public MockPool(T prefab, Transform parentTransform)
        {
            _prefab = prefab;
            _parentTransform = parentTransform;
        }

        public T Get()
        {
            var item = UnityEngine.Object.Instantiate(_prefab);
            item.transform.SetParent(_parentTransform);

            return UnityEngine.Object.Instantiate(_prefab);
        }

        public void Release(T item)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }
}