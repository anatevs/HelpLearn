using UnityEngine;

namespace GameManagement
{
    public class DDOLClass<T> : DDOLAbstract
        where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance => _instance;

        public override void CreateInstance(GameObject prefab)
        {
            if (_instance == null)
            {
                GameObject instanceGO = Instantiate(prefab);

                _instance = instanceGO.GetComponent<T>();
                _instance.name = _instance.GetType().Name;

                DontDestroyOnLoad(instanceGO);
            }
        }
    }
}