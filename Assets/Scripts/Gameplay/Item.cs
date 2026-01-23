using Events;
using System;
using UnityEngine;

namespace Gameplay
{
    public class Item : MonoBehaviour
    {
        public event Action<Item> OnPicked;

        public string Name { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var _))
            {
                OnPicked?.Invoke(this);
            }
        }
    }
}