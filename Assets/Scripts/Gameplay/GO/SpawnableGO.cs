using System;
using UnityEngine;

namespace Gameplay
{
    public class SpawnableGO : MonoBehaviour
    {
        public event Action<string> OnUnspawned;

        public string Name { get; set; }

        protected void MakeUnspawn()
        {
            OnUnspawned?.Invoke(Name);
        }
    }
}