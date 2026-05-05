using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class MovementTransform : IMovementService
    {
        public GameObject Movable => _movable;

        private readonly GameObject _movable;

        public MovementTransform(GameObject movable)
        {
            _movable = movable;
        }

        public void MoveFixedUpdate(Vector3 moveDirection, float speed) {}

        public void MoveUpdate(Vector3 moveDirection, float speed)
        {
            _movable.transform.Translate(Time.deltaTime * speed * moveDirection, Space.Self);
        }
    }
}