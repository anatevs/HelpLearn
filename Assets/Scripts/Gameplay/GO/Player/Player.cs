using Assets.Input;
using GameManagement;
using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MoveRBComponent))]
    [RequireComponent(typeof(HPComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    [RequireComponent(typeof(FallComponent))]
    public sealed class Player : MonoBehaviour, IPausable
    {
        public event Action OnPlayerKilled;

        public HPComponent HP => _hp;

        public WeaponComponent Weapon => _weapon;

        public PlayerDataConfig DataConfig => _dataConfig;

        [SerializeField]
        private PlayerMovementConfig _movementConfig;

        [SerializeField]
        private PlayerDataConfig _dataConfig;

        [SerializeField]
        private RotationComponent _viewRotation;

        [SerializeField]
        private Collider _undergroundPlane;

        private InputHandler _input;

        private HPComponent _hp;

        private WeaponComponent _weapon;

        private MoveRBComponent _movement;

        private FallComponent _fall;

        private Vector3 _lookDirection;

        private WeaponStorage _weaponStorage;

        private Vector3 _startPosition;

        private bool _isPlaying = false;

        private void OnDisable()
        {
            _input.OnJupmed -= _movement.Jump;
            _input.OnShoot -= Shoot;

            _hp.OnHPChanged -= HandleDamage;
            _weaponStorage.OnWeaponChanged -= ChangeWeapon;

            _fall.OnFell -= MakeKill;
        }

        private void Update()
        {
            if (!_isPlaying)
            {
                return;
            }

            _lookDirection = _input.GetLookDirection(transform);

            _viewRotation.Rotate(_lookDirection, _movementConfig.RotationSpeed, Time.deltaTime);

            _fall.CheckFallUpd();
        }

        private void FixedUpdate()
        {
            _movement.MoveFixedUpd(_input.Move);
        }

        public void Construct(WeaponStorage weaponStorage, InputHandler input)
        {
            _input = input;

            _movement = GetComponent<MoveRBComponent>();
            _movement.Construct(_movementConfig);

            _hp = GetComponent<HPComponent>();
            _weapon = GetComponent<WeaponComponent>();
            _fall = GetComponent<FallComponent>();

            _startPosition = transform.position;

            _weaponStorage = weaponStorage;

            _input.OnJupmed += _movement.Jump;
            _input.OnShoot += Shoot;

            _hp.OnHPChanged += HandleDamage;
            _weaponStorage.OnWeaponChanged += ChangeWeapon;

            _fall.OnFell += MakeKill;

            InitPlayer();
        }

        public void ResetLevel()
        {
            InitPlayer();

            Resume();

            _movement.ResetLevel();
        }

        public void Pause()
        {
            _movement.Pause();

            _isPlaying = false;
        }

        public void Resume()
        {
            _movement.Resume();

            _isPlaying = true;
        }

        public void ChangeWeapon(Weapon newWeapon)
        {
            _weapon.SetWeapon(newWeapon);
        }

        private void InitPlayer()
        {
            _hp.Init(_dataConfig.StartHP);

            transform.position = _startPosition;
        }

        private void Shoot()
        {
            if (!_isPlaying)
            {
                return;
            }

            _weapon.Shoot();
        }

        private void HandleDamage(int newHP)
        {
            if (newHP == 0)
            {
                MakeKill();
            }
        }

        private void MakeKill()
        {
            OnPlayerKilled?.Invoke();

            Pause();
        }
    }
}