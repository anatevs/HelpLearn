using EventBusNamespace;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class Item : MonoBehaviour, INamed
    {
        public string Name => _config.Name;

        [SerializeField]
        private ItemConfig _config;

        private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
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