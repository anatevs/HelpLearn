using GameManagement;
using Gameplay;
using System;
using UnityEngine;

namespace Input
{
    public interface IInputService :
        IResetable,
        IDisposable
    {
        public string TypeName { get; }
        public float Speed { get; }
        public float RotationSpeed { get; }

        public Vector3 Move { get; }

        public Vector3 LookDirection { get; }

        public void Update(Transform movable);

        public void Disable();
    }
}