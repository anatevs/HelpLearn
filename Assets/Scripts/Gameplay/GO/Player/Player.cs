using Assets.Input;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MoveRBComponent))]
    [RequireComponent(typeof(HPComponent))]
    [RequireComponent(typeof(ShotComponent))]
    public sealed class Player : MonoBehaviour
    {
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

        public void Construct(ProjectileSpawnService projectileService)
        {
            _projectileService = projectileService;

            _shot.Init(_projectileService);
        }

        public void Init()
        {
            _hp.Init(_dataConfig.StartHP);
        }

        private void Awake()
        {
            _movement = GetComponent<MoveRBComponent>();
            _movement.Init(_movementConfig);

            _hp = GetComponent<HPComponent>();
            _shot = GetComponent<ShotComponent>();
        }

        private void OnEnable()
        {
            _input.OnJupmed += _movement.Jump;
            _input.OnShoot += Shoot;
        }

        private void OnDisable()
        {
            _input.OnJupmed -= _movement.Jump;
            _input.OnShoot -= Shoot;
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

        private void Shoot()
        {
            _shot.Shoot(_lookDirection);
        }
    }
}