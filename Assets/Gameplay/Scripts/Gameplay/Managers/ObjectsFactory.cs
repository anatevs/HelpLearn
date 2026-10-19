using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay
{
    public abstract class ObjectsFactory<T> : IObjectsFactory<T> where T : Component, INamed
    {
        public IEnumerable<string> Names => _prefabs.Keys;

        protected readonly IObjectResolver _container;

        protected readonly Dictionary<string, T> _prefabs = new();

        public ObjectsFactory(IObjectResolver resolver, T[] prefabs)
        {
            _container = resolver;

            for (int i = 0; i < prefabs.Length; i++)
            {
                _prefabs.Add(prefabs[i].Name, prefabs[i]);
            }
        }

        public abstract T Create(string name);

        protected virtual T GetFromPrefab(string name)
        {
            return _container.Instantiate(_prefabs[name]);
        }
    }
}