using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class MovablesSystem : MonoBehaviour
    {
        private readonly HashSet<IMovableFixedUpd> _movablesFxUpd = new();
        private readonly HashSet<IMovableUpd> _movableUpd = new();

        public void AddMovable(IMovableFixedUpd movable)
        {
            _movablesFxUpd.Add(movable);
        }

        public void AddMovable(IMovableUpd movable)
        {
            _movableUpd.Add(movable);
        }

        public void RemoveMovable(IMovableFixedUpd movable)
        {
            _movablesFxUpd.Remove(movable);
        }

        public void RemoveMovable(IMovableUpd movable)
        {
            _movableUpd.Remove(movable);
        }

        private void Update()
        {
            foreach (var movable in _movableUpd)
            {
                movable.MoveUpdate();
            }
        }

        private void FixedUpdate()
        {
            foreach (var movable in _movablesFxUpd)
            {
                movable.MoveFixedUpd();
            }
        }
    }
}