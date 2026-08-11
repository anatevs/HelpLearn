using Mirror;
using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class PickableItem : NetworkBehaviour
    {
        public event Action<PickableItem> OnPicked;

        [SyncVar]
        public uint SpawnId;

        public ItemConfig Config => _config;

        private ItemConfig _config;

        private uint _spawnId;

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