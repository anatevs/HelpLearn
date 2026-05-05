using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class Item : MonoBehaviour,
        IItem
    {
        public event Action<IItem> OnCollected;

        public ItemConfig Config => _config;

        public string Name => _config.Name;
        public int Amount => _config.Amount;

        [SerializeField]
        private ItemConfig _config;

        public void Collect()
        {
            OnCollected?.Invoke(this);
        }

        public void Deactivate()
        {
            Destroy(gameObject);
        }
    }
}