using UnityEngine;

namespace GameManagement
{
    public abstract class DDOLClass<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance => _instance;

        public static void CreateInstance(string prefabPathName)
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<GameObject>(prefabPathName);

                if (prefab == null)
                {
                    Debug.LogError($"no prefab for {typeof(T)} in resources folder. Searching path: {prefabPathName}");
                }

                GameObject instanceGO = Instantiate(prefab);

                _instance = instanceGO.GetComponent<T>();
                _instance.name = _instance.GetType().Name;

                DontDestroyOnLoad(instanceGO);
            }
        }
    }
}