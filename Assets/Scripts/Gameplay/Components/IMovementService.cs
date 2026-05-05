using UnityEngine;

namespace Gameplay
{
    public interface IMovementService
    {
        public GameObject Movable { get; }
        public void MoveUpdate(Vector3 moveDirection, float speed);
        public void MoveFixedUpdate(Vector3 moveDirection, float speed);
    }
}