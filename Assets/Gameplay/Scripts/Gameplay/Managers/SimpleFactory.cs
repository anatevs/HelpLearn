using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public class SimpleFactory<T> : ObjectsFactory<T> where T : MonoBehaviour, INamed
    {
        public SimpleFactory(IObjectResolver resolver, IPrefabConfig<T>[] configs)
            : base(resolver, configs)
        {

        }

        public override T Create(string name)
        {
            return GetFromPrefab(name);
        }
    }
}