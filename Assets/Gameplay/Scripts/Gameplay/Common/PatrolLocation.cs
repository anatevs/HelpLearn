using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public struct PatrolLocation
    {
        public Transform CentralPoint;

        public Transform[] Points;
    }
}