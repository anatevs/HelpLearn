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

        private ClientPool<Grenade> _pool;

        public void Init(PickItemsSpawnConfig pickItemsSpawnConfig)
        {
            _spawnConfig = pickItemsSpawnConfig;

            _grenadeConfig = (GrenadeConfig)_spawnConfig.GetConfig(ItemType.Grenade);
            _grenadePrefab = _grenadeConfig.GrenadePrefab;

            _pool = new ClientPool<Grenade>(_grenadePrefab, _grenadeConfig.InitPoolSize, _poolTransform, transform);
        }

        private void OnDestroy()
        {
            _pool.Dispose();
        }

        [Server]
        public void Spawn(Transform throwPoint, string throwerName)
        {
            var grenade = _pool.Spawn();

            grenade.transform.SetPositionAndRotation(throwPoint.position, throwPoint.rotation);

            NetworkServer.Spawn(grenade.gameObject);

            grenade.Init(_grenadeConfig);

            grenade.OnExploded += Unspawn;

            grenade.Throw(throwerName);
        }

        [Server]
        public void Unspawn(Grenade grenade)
        {
            grenade.OnExploded -= Unspawn;

            _pool.Unspawn(grenade);
        }
    }
}