using Mirror;
using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class PickableItem : NetworkBehaviour
    {
        public event Action<PickableItem> OnPicked;

        public ItemConfig Config => _config;

        private ItemConfig _config;

        public void Init(ItemConfig config)
        {
            _config = config;
        }

        [Server]
        public void Pick()
        {
            OnPicked?.Invoke(this);
        }
    }
}