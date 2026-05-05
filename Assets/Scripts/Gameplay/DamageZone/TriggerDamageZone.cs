using UnityEngine;

namespace Gameplay
{
    public sealed class TriggerDamageZone : MonoBehaviour,
        IDamageZone
    {
        public int Damage => _damage;

        [SerializeField]
        private int _damage;

        public void CheckAndMakeDamage(GameObject checkGO)
        {
            if (checkGO.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.Health.TakeDamage(_damage);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckAndMakeDamage(other.gameObject);
        }
    }
}