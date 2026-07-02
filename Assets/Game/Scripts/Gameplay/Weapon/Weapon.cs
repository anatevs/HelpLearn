using Mirror;
using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Weapon : NetworkBehaviour
    {
        public event Action<int> OnChargeSet;

        public event Action<float> OnRecharchStarted;
        public event Action OnRechargeEnded;

        public Vector3 ShootPoint => _shootPoint.position;

        public string Name => _config.Name;

        public WeaponConfig Config => _config;

        public int CurrentCharge => _currentCharge;

        [SyncVar(hook = nameof(HookSetCharge))]
        private int _currentCharge = 0;

        [SerializeField]
        private WeaponConfig _config;

        [SerializeField]
        private Transform _shootPoint;

        [SerializeField]
        private ParticleSystem _shootFlash;

        [SerializeField]
        private GameObject _weaponVisual;

        private WeaponTracerShower _tracerShower;

        private bool _isCooldowning = false;

        private bool CanShoot => !_isCooldowning && _currentCharge > 0;

        private WaitForSeconds _cooldownWait;

        private void Awake()
        {
            _cooldownWait = new WaitForSeconds(_config.FireRate);
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            ChangeCharge(_config.Charge);
        }

        public void Init(WeaponTracerShower tracerShower)
        {
            _tracerShower = tracerShower;
        }

        public void ShowVisual(bool isShow)
        {
            _weaponVisual.SetActive(isShow);
        }

        [Server]
        public void ChangeCharge(int value)
        {
            _currentCharge += value;

            _currentCharge = Mathf.Clamp(_currentCharge, 0, _config.Charge);

            if (_currentCharge == 0)
            {
                var endTime = (float)NetworkTime.time + _config.RechargeTime;

                StartCoroutine(RechargeCoroutine(endTime));
            }
        }

        [Command]
        public void Shoot(Vector3 viewPoint, Vector3 direction)
        {
            if (CanShoot)
            {
                RpcTrgShowFlash();
                ChangeCharge(-_config.ShootCost);

                if (Physics.Raycast(viewPoint, direction, out var hit, _config.MaxDistance))
                {
                    if (hit.collider.gameObject.TryGetComponent<Health>(out var hp))
                    {
                        hp.TakeDamage(_config.Damage);

                        StartCoroutine(Cooldown());

                        var endPoint = hit.point;

                        _tracerShower.ShowTrace(_shootPoint.position, endPoint, Name);
                    }
                }
            }
        }

        [TargetRpc]
        private void RpcTrgShowFlash()
        {
            _shootFlash.Play();
        }

        [TargetRpc]
        private void RpcTrgStartRecharge(float endTime)
        {
            OnRecharchStarted?.Invoke(endTime);
        }

        [TargetRpc]
        private void RpcTrgEndRecharge()
        {
            OnRechargeEnded?.Invoke();
        }

        private IEnumerator Cooldown()
        {
            _isCooldowning = true;

            yield return _cooldownWait;

            _isCooldowning = false;
        }

        private IEnumerator RechargeCoroutine(float endTime)
        {
            RpcTrgStartRecharge(endTime);

            while (endTime - (float)NetworkTime.time > 0)
            {
                yield return null;
            }

            ChangeCharge(_config.Charge);

            RpcTrgEndRecharge();
        }

        private void HookSetCharge(int oldCharge, int newCharge)
        {
            OnChargeSet?.Invoke(newCharge);
        }
    }
}