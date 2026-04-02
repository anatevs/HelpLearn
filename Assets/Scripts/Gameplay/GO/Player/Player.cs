using Assets.Input;
using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MoveRBComponent))]
    [RequireComponent(typeof(HPComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class Player : MonoBehaviour
    {
        public event Action OnPlayerKilled;

        public HPComponent HP => _hp;

        public WeaponComponent Weapon => _weapon;

        public PlayerDataConfig DataConfig => _dataConfig;

        [SerializeField]
        private InputHandler _input;

        [SerializeField]
        private PlayerMovementConfig _movementConfig;

        [SerializeField]
        private PlayerDataConfig _dataConfig;

        [SerializeField]
        private RotationComponent _viewRotation;

        private HPComponent _hp;

        private WeaponComponent _weapon;

        private MoveRBComponent _movement;

        private Vector3 _lookDirection;

        private WeaponStorage _weaponStorage;

        private Vector3 _startPosition;


        [SerializeField]
        private WeaponConfig _weaponConfig;




        private void OnEnable()
        {
            _input.OnJupmed += _movement.Jump;
            _input.OnShoot += Shoot;

            _hp.OnHPChanged += HandleDamage;
            _weaponStorage.OnWeaponChanged += ChangeWeapon;
        }

        private void OnDisable()
        {
            _input.OnJupmed -= _movement.Jump;
            _input.OnShoot -= Shoot;

            _hp.OnHPChanged -= HandleDamage;
            _weaponStorage.OnWeaponChanged -= ChangeWeapon;
        }

        private void Update()
        {
            _lookDirection = (_input.LookPoint - transform.position).normalized;

            _lookDirection.y = 0;

            _viewRotation.Rotate(_lookDirection, _movementConfig.RotationSpeed, Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _movement.MoveFixedUpd(_input.Move);
        }

        public void Construct(WeaponStorage weaponStorage)
        {
            _movement = GetComponent<MoveRBComponent>();
            _movement.Construct(_movementConfig);

            _hp = GetComponent<HPComponent>();
            _weapon = GetComponent<WeaponComponent>();

            _startPosition = transform.position;

            _weaponStorage = weaponStorage;

            InitPlayer();
        }

        public void ResetLevel()
        {
            InitPlayer();

            _movement.ResetLevel();
        }

        public void Pause()
        {
            _movement.Pause();
        }

        public void Resume()
        {
            _movement.Resume();
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
            _weapon.Shoot();
        }

        private void HandleDamage(int newHP)
        {
            if (newHP == 0)
            {
                OnPlayerKilled?.Invoke();

                Pause();
            }
        }
    }
}