using System;
using UnityEngine;

namespace Gameplay
{
    public sealed class MovementRB : IMovementService
    {
        public GameObject Movable => _movable;

        private readonly GameObject _movable;
        private readonly Rigidbody _rb;

        public MovementRB(GameObject movable)
        {
            _movable = movable;

            if (!_movable.TryGetComponent<Rigidbody>(out _rb))
            {
                throw new Exception($"No Rigidbody component on {_movable} game object. Cannot move it by {this.GetType()}");
            }
        }

        public void MoveFixedUpdate(Vector3 moveDirection, float speed)
        {
            _rb.AddRelativeForce(speed * moveDirection);
        }

        public void MoveUpdate(Vector3 moveDirection, float speed) {}

        public void ResetLevel()
        {
            _rb.isKinematic = true;
            _rb.isKinematic = false;
        }
    }
}