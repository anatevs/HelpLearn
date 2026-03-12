using UnityEngine;

namespace GameManagement
{
    public abstract class DDOLAbstract : MonoBehaviour
    {
        public abstract void CreateInstance(GameObject prefab);
    }
}