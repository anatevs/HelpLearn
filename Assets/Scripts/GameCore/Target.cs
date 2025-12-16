using UnityEngine;

namespace GameCore
{
    public sealed class Target : MonoBehaviour
    {
        private float _destroyTime = 3f;

        private void Start()
        {
            Debug.Log($"Target created");
            Destroy(gameObject, _destroyTime);
        }

        private void Update()
        {
            Debug.Log($"Target still alive");
        }

        private void OnDestroy()
        {
            Debug.Log($"Target destroyed");
        }
    }
}