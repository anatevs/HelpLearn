using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class SimpleFactory<T> : ObjectsFactory<T> where T : Component, INamed
    {
        public SimpleFactory(IObjectResolver resolver, T[] prefabs)
            : base(resolver, prefabs)
        {
        }

        public override T Create(string name)
        {
            return GetFromPrefab(name);
        }
    }
}