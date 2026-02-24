using EventBusNamespace;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class Item : MonoBehaviour
    {
        public ItemConfig Config => _config;

        [SerializeField]
        private ItemConfig _config;

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Player>(out var _))
            {
                EventBus.RaiseEvent(new ItemPickedEvent(this));
            }
        }
    }
}