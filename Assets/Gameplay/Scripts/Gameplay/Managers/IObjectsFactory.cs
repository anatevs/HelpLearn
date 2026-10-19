using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public interface IObjectsFactory<T> where T : Component, INamed
    {
        public IEnumerable<string> Names { get; }
        public T Create(string name);
    }
}