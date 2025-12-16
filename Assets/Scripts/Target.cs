using UnityEngine;

namespace GameCore
{
    public sealed class Target : MonoBehaviour
    {
        private float _destroyTime = 3f;

        private void Start()
        {
            Debug.Log($"{gameObject.name}'s start");
            Destroy(gameObject, _destroyTime);
        }

        private void Update()
        {
            Debug.Log($"{gameObject.name} is updating");
        }

        private void OnDestroy()
        {
            Debug.Log($"{gameObject.name}'s destroy");
        }
    }
}