using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(RotationComponent))]
    public sealed class Turret : Weapon
    {
        [SerializeField]
        private SphereTriggerComponent _sphereTrigger;

        [SerializeField]
        private TurretConfig _turretConfig;

        private RotationComponent _rotation;

        private Quaternion _startRotation;

        private bool _isTargetInRadius = false;

        private TurretParams _turretParams;

        private Transform _target;

        private void OnDisable()
        {
            _sphereTrigger.OnEntered -= HandleEntered;
            _sphereTrigger.OnExited -= HandleExited;
        }

        public void Construct(ProjectileSpawnService spawnService, Transform target)
        {
            base.Construct(spawnService);
            _target = target;

            _sphereTrigger.OnEntered += HandleEntered;
            _sphereTrigger.OnExited += HandleExited;

            _startRotation = transform.rotation;
            _rotation = GetComponent<RotationComponent>();
        }

        public void Init()
        {
            Init(_turretConfig.WeaponParams, _turretConfig.TurretParams);
        }

        public void Init(WeaponParams weaponParams, TurretParams turretParams)
        {
            _turretParams = turretParams;
            _sphereTrigger.SetRadius(_turretParams.DetectionRadius);
            ChangeActive(true);

            base.Init(weaponParams);
        }

        public override void ResetLevel()
        {
            base.ResetLevel();

            transform.rotation = _startRotation;
            _isTargetInRadius = false;
        }

        private void Update()
        {
            if (_isTargetInRadius)
            {
                var direction = _target.position - _appearance.ShotPoint.position;
                direction.y = 0;
                direction.Normalize();

                if (Physics.Raycast(_appearance.ShotPoint.position, direction, out var hit, _turretParams.DetectionRadius))
                {
                    if (hit.collider.transform == _target)
                    {
                        _rotation.Rotate(direction, _turretParams.RotationSpeed, Time.deltaTime);

                        Shoot();
                    }
                }
            }
        }

        protected override void SetRemain(int newCapacity)
        {
            base.SetRemain(_weaponParams.Capacity);
        }

        private void HandleEntered(Transform entered)
        {
            if (!_isTargetInRadius && _target == entered)
            {
                _isTargetInRadius = true;
            }
        }

        private void HandleExited(Transform exited)
        {
            if (_target == exited)
            {
                _isTargetInRadius = false;
            }
        }
    }
}