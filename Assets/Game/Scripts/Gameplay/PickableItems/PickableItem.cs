using Mirror;
using System;
using UnityEngine;

namespace Gameplay
{
    public class PickableItem : NetworkBehaviour
    {
        public event Action<PickableItem> OnPicked;

        public ItemConfig Config => _config;

        public Collider Collider => _collider;

        [SerializeField]
        private ItemConfig _config;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        [Server]
        public void Pick()
        {
            OnPicked?.Invoke(this);
        }
    }
}