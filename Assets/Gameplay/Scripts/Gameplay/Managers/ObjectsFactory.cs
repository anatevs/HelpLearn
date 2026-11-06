using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay
{
    public abstract class ObjectsFactory<T> : IObjectsFactory<T> where T : MonoBehaviour, INamed
    {
        public IEnumerable<string> Names => _configs.Keys;

        protected readonly IObjectResolver _container;

        protected readonly Dictionary<string, T> _prefabs = new();


        protected readonly Dictionary<string, IPrefabConfig<T>> _configs = new();

        protected ObjectsFactory(IObjectResolver resolver, IPrefabConfig<T>[] configs)
        {
            _container = resolver;

            SetConfigs(configs);
        }

        public void SetConfigs(IPrefabConfig<T>[] configs)
        {
            _configs.Clear();

            for (int i = 0; i < configs.Length; i++)
            {
                _configs.Add(configs[i].Name, configs[i]);
            }
        }

        public abstract T Create(string name);

        protected virtual T GetFromPrefab(string name)
        {
            return _container.Instantiate(_configs[name].Prefab);
        }
    }
}