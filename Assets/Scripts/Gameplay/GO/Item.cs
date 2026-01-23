using UnityEngine;

namespace Gameplay
{
    public class Item : SpawnableGO
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var _))
            {
                MakeUnspawn();
            }
        }
    }
}