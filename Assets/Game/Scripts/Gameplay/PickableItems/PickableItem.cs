using Mirror;
using UnityEngine;

namespace Gameplay
{
    public class PickableItem : NetworkBehaviour
    {
        public ItemConfig Config => _config;

        public Collider Collider => _collider;

        [SerializeField]
        private ItemConfig _config;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Pick()
        {
            NetworkServer.Destroy(gameObject);
            //Destroy(gameObject);
        }
    }
}