using System.Collections;
using UnityEngine;

namespace EventBusNamespace
{
    public class GameEventT<T> : GameEvent
    {
        public T Value => _value;

        private readonly T _value;

        public GameEventT(T value)
        {
            _value = value;
        }
    }
}