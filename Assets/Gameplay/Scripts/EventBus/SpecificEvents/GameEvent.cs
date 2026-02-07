using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class GameEvent
    {
        public string Name => _name;

        private readonly string _name;
    }
}