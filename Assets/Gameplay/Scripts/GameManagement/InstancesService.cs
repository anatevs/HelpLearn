using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "InstancesService",
        menuName = "Configs/GameSystem/InstancesService")]
    public sealed class InstancesService : ScriptableObject
    {
        private Dictionary<Type, System.Object> _instances = new();

        public T GetInstance<T>()
        {
            var type = typeof(T);
            T result = default;

            if (!_instances.TryGetValue(type, out var resultObject))
            {
                Debug.LogError($"no registered value of type {type} in DI");
            }

            result = (T)resultObject;

            return result;
        }

        public void AddInstanceType<T>(T instance)
        {
            if (!_instances.TryAdd(typeof(T), instance))
            {
                Debug.LogWarning($"there is also registered MonoBehaviour of type {typeof(T)} in DI");
                return;
            }
        }

        public bool ContainsType(Type type)
        {
            return _instances.ContainsKey(type);
        }
    }
}