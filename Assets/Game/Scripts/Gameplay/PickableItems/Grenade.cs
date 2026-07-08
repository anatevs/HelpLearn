using Mirror;
using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class Grenade : NetworkBehaviour
    {
        public event Action<Grenade> OnExploded;

        [SerializeField]
        private GameObject _grenadeVisual;

        [SerializeField]
        private ParticleSystem _explosionVFX;

        private string _currentThrower;

        private GrenadeConfig _config;

        private Rigidbody _rigidbody;

        private readonly Collider[] _damagedColliders = new Collider[10];

        private Vector3 _gravity;

        private WaitForSeconds _showVfxWait;

        private ParticleSystem.MainModule _mainModule;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _mainModule = _explosionVFX.main;
        }

        public void Init(GrenadeConfig config)
        {
            _config = config;

            _mainModule.startSize = _config.DamageRadius;

            _showVfxWait = new WaitForSeconds(_mainModule.duration);

            if (!isServer)
            {
                _rigidbody.isKinematic = false;
            }
        }

        private void FixedUpdate()
        {
            if (isServer)
            {
                _rigidbody.AddForce(_gravity, ForceMode.Acceleration);
            }
        }

        [Server]
        public void Throw(string throwerName)
        {
            _grenadeVisual.SetActive(true);

            _gravity = Physics.gravity * _config.GravityMultiplier;

            _rigidbody.AddForce(_config.ThrowSpeed * transform.forward, ForceMode.Impulse);

            StartCoroutine(WaitExplosionCoroutine());

            _currentThrower = throwerName;
        }

        [Server]
        private IEnumerator WaitExplosionCoroutine()
        {
            yield return _config.ExplosionWait;

            ShowVFX(_config.DamageRadius);

            yield return _showVfxWait;

            MakeDamage();

            OnExploded?.Invoke(this);
        }

        [Server]
        private void MakeDamage()
        {
            var count = Physics.OverlapSphereNonAlloc(
                transform.position,
                _config.DamageRadius,
                _damagedColliders,
                _config.DamageLayers);

            for (int i = 0; i < count; i++)
            {
                if (_damagedColliders[i].TryGetComponent<Health>(out var health))
                {
                    health.TakeDamage(_config.Damage, _currentThrower);
                }
            }
        }

        [ClientRpc]
        private void ShowVFX(float radius)
        {
            _grenadeVisual.SetActive(false);
            _mainModule.startSize = radius;
            _explosionVFX.Play();
        }
    }
}