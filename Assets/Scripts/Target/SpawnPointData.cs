using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public struct SpawnPointData
    {
        public readonly float StartDelay => _startDelay;
        public readonly float SpawnPeriod => _spawnPeriod;
        public readonly float TargetSpeed => _targetSpeed;

        [SerializeField]
        private float _startDelay;

        [SerializeField]
        private float _spawnPeriod;

        [SerializeField]
        private float _targetSpeed;
    }
}