using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class LifetimedSystem : MonoBehaviour
    {
        private readonly HashSet<ILifetimed> _lifetimed = new();

        private readonly List<ILifetimed> _removeQueue = new();

        private int _toRemoveCount = 0;

        public void AddLifetimed(ILifetimed lifetimed)
        {
            _lifetimed.Add(lifetimed);
        }

        public void RemoveLifetimed(ILifetimed lifetimed)
        {
            _toRemoveCount++;

            if (_removeQueue.Count >= _toRemoveCount)
            {
                _removeQueue[_toRemoveCount - 1] = lifetimed;
            }
            else
            {
                _removeQueue.Add(lifetimed);
            }
        }

        private void Update()
        {
            if (_toRemoveCount > 0)
            {
                for (int i = _toRemoveCount - 1; i >= 0; i--)
                {
                    _lifetimed.Remove(_removeQueue[i]);
                }
                _toRemoveCount = 0;
            }

            foreach (var lifetimed in _lifetimed)
            {
                lifetimed.CheckLifetime(Time.deltaTime);
            }
        }
    }
}