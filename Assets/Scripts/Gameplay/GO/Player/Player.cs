using Assets.Input;
using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MoveRBComponent))]
    [RequireComponent(typeof(HPComponent))]
    [RequireComponent(typeof(ShotComponent))]
    public sealed class Player : MonoBehaviour
    {
        public event Action OnPlayerKilled;

        public HPComponent HP => _hp;

        public CharacterDataConfig DataConfig => _dataConfig;

        [SerializeField]
        private InputHandler _input;

        [SerializeField]
        private PlayerMovementConfig _movementConfig;

        [SerializeField]
        private CharacterDataConfig _dataConfig;

        [SerializeField]
        private RotationComponent _viewRotation;

        private HPComponent _hp;

        private ShotComponent _shot;

        private MoveRBComponent _movement;

        private Vector3 _lookDirection;

        private ProjectileSpawnService _projectileService;

        private Vector3 _startPosition;

        private void OnEnable()
        {
            _input.OnJupmed += _movement.Jump;
            _input.OnShoot += Shoot;

            _hp.OnHPChanged += HandleDamage;
        }

        private void OnDisable()
        {
            _input.OnJupmed -= _movement.Jump;
            _input.OnShoot -= Shoot;

            _hp.OnHPChanged -= HandleDamage;
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

        public void Construct(ProjectileSpawnService projectileService)
        {
            _movement = GetComponent<MoveRBComponent>();
            _movement.Construct(_movementConfig);

            _hp = GetComponent<HPComponent>();
            _shot = GetComponent<ShotComponent>();

            _startPosition = transform.position;



            _projectileService = projectileService;

            _shot.Construct(_projectileService);
        }

        public void Init()
        {
            _hp.Init(_dataConfig.StartHP);

            transform.position = _startPosition;

            _movement.Init();
        }

        public void Pause()
        {
            _movement.Pause();
        }

        public void Resume()
        {
            _movement.Resume();
        }

        private void Shoot()
        {
            _shot.Shoot(_lookDirection);
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