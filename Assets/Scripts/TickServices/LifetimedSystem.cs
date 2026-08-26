using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class LifetimedSystem : MonoBehaviour
    {
        private readonly HashSet<ILifetimed> _lifetimed = new();

        private readonly List<ILifetimed> _removeQueue = new();

        public void AddLifetimed(ILifetimed lifetimed)
        {
            _lifetimed.Add(lifetimed);
        }

        public void RemoveLifetimed(ILifetimed lifetimed)
        {
            _removeQueue.Add(lifetimed);
        }

        private void Update()
        {
            if (_removeQueue.Count > 0)
            {
                for (int i = _removeQueue.Count - 1; i >= 0; i--)
                {
                    _lifetimed.Remove(_removeQueue[i]);
                    _removeQueue.RemoveAt(i);
                }
            }

            foreach (var lifetimed in _lifetimed)
            {
                lifetimed.CheckLifetime(Time.deltaTime);
            }
        }
    }
}