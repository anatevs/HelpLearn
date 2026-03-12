using UnityEngine;

namespace GameManagement
{
    public sealed class DDOLBootstrap : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _prefabsDDOL;

        private void Awake()
        {
            foreach (var prefabGO in _prefabsDDOL)
            {
                if (!prefabGO.TryGetComponent<DDOLAbstract>(out var prefabDDOL))
                {
                    Debug.LogWarning($"prefab {prefabGO} does not contain DDOL component");
                    return;
                }

                prefabDDOL.CreateInstance(prefabGO);
            }
        }
    }
}