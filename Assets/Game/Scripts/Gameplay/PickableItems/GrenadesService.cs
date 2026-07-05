using Mirror;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class GrenadesService : NetworkBehaviour
    {
        [SerializeField]
        private Transform _poolTransform;

        private PickItemsSpawnConfig _spawnConfig;

        private Grenade _grenadePrefab;
        private GrenadeConfig _grenadeConfig;

        public void Init(PickItemsSpawnConfig pickItemsSpawnConfig)
        {
            _spawnConfig = pickItemsSpawnConfig;

            _grenadeConfig = (GrenadeConfig)_spawnConfig.GetConfig(ItemType.Grenade);
            _grenadePrefab = _grenadeConfig.GrenadePrefab;

            NetworkClient.RegisterPrefab(_grenadePrefab.gameObject);
        }

        [Server]
        public void Spawn(Transform throwPoint)
        {
            Grenade grenade = Instantiate(_grenadePrefab, throwPoint.position, throwPoint.rotation, transform);

            NetworkServer.Spawn(grenade.gameObject);

            grenade.Init(_grenadeConfig);

            grenade.OnExploded += Unspawn;

            grenade.Throw();
        }

        [Server]
        public void Unspawn(Grenade grenade)
        {
            grenade.OnExploded -= Unspawn;
            grenade.gameObject.SetActive(false);
            grenade.transform.SetParent(_poolTransform, false);

            NetworkServer.Destroy(grenade.gameObject);
        }
    }
}