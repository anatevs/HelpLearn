using EventBusNamespace;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class Item : MonoBehaviour
    {
        public ItemConfig Config => _config;

        [SerializeField]
        private ItemConfig _config;

        private EventBus _eventBus;

        public void Init(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Player>(out var _))
            {
                _eventBus.RaiseEvent(new ItemPickedEvent(this));
            }
        }
    }
}