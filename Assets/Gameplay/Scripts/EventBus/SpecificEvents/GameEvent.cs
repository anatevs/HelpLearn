using System.Collections;
using UnityEngine;

namespace EventBusNamespace
{
    public class GameEvent : IGameEvent
    {
        public string Name => _name;

        public string Description => _description;

        public string EventType => this.GetType().Name;

        protected string _name;

        protected string _description;
    }
}