using Assets.Input;
using Gameplay;
using Mirror;
using System;
using System.Collections;
using UnityEngine;

namespace GameManagement
{
    public class GamePlayer : NetworkBehaviour
    {
        public event Action<float> OnRespawnCooldownStarted;
        public event Action OnRespawnCooldownCompleted;
        public event Action<ItemType, int> OnInventoryUpdated;
        public event Action<string, string> OnItemPicked;
        public event Action<string> OnAbsentItemTried;

        public Health Health => _health;
        public Weapon Weapon => _weapon;
        public InventoryStorage InventoryStorage => _inventoryStorage;

        [SyncVar(hook = nameof(SetName))]
        public string Name = "nameDefault";

        [SyncVar(hook = nameof(SetColor))]
        public Color Color = Color.black;

        [SerializeField]
        private PlayerVisual _playerVisual;

        [SerializeField]
        private PlayerMoveController _playerMoveController;

        [SerializeField]
        private PlayerRotation _playerRotation;

        [SerializeField]
        private Transform _cameraPoint;

        [SerializeField]
        private Health _health;

        [SerializeField]
        private Weapon _weapon;

        [SerializeField]
        private Transform _grenadePoint;

        private CameraFollower _cameraFollower;

        private InputHandler _input;

        private WeaponTracerShower _weaponTracerShower;

        private Collider _collider;
        private Rigidbody _rigidbody;

        private Vector3 _startPosition;

        private bool _useMedkitCooldown = true;

        private bool _useGrenadeCooldown = true;

        private PickItemsSpawnConfig _pickItemsConfig;

        private GrenadesService _grenadesService;

        private readonly InventoryStorage _inventoryStorage = new();

        public bool Construct(CameraFollower cameraFollower,
            InputHandler input,
            WeaponTracerShower weaponTracerShower,
            PickItemsSpawnConfig pickItemsConfig,
            GrenadesService grenadesService)
        {
            if (isLocalPlayer)
            {
                _cameraFollower = cameraFollower;
                _input = input;

                _cameraFollower.SetPoint(_cameraPoint);

                _playerMoveController.Init(_input);
                _playerRotation.Init(_input);

                _input.OnShot += HandleShoot;

                _input.OnHealed += CmdHeal;

                _input.OnGrenadeThrown += CmdThrowGrenade;
            }

            _weaponTracerShower = weaponTracerShower;
            _weapon.Init(_weaponTracerShower);

            _pickItemsConfig = pickItemsConfig;
            _grenadesService = grenadesService;

            return isLocalPlayer;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();

            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            _health.OnKilled += HandleKill;
            _inventoryStorage.OnEmptyAccessed += TargetTryUseAbsentItem;
        }

        private void OnDisable()
        {
            if (_input != null)
            {
                _input.OnShot -= HandleShoot;
                _input.OnHealed -= CmdHeal;
                _input.OnGrenadeThrown -= CmdThrowGrenade;
            }

            _health.OnKilled -= HandleKill;
            _inventoryStorage.OnEmptyAccessed -= TargetTryUseAbsentItem;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            _playerVisual.SetName(Name);

            _playerVisual.SetColor(Color);

            if (!isLocalPlayer)
            {
                _rigidbody.isKinematic = true;
            }

            if (NetworkManager.singleton is LobbyManager lobbyManager)
            {
                lobbyManager.RegisterGamePlayer(this);
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            _inventoryStorage.OnItemUpdated += TargetUpdateInventory;
        }

        public override void OnStopServer()
        {
            base.OnStopServer();

            _inventoryStorage.OnItemUpdated -= TargetUpdateInventory;
        }

        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            _playerMoveController.UpdateMovement();
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            _playerMoveController.FixedUpdateMovement();
            _playerRotation.FixedUpdateRotation();
        }

        private void LateUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            _cameraFollower.Follow();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isLocalPlayer)
            {
                return;
            }

            if (other.gameObject.TryGetComponent<PickableItem>(out var pickedItem))
            {
                CmdPickItem(pickedItem.netId);
            }
        }

        private void SetName(string oldName, string newName)
        {
            _playerVisual.SetName(newName);
        }

        private void SetColor(Color oldColor, Color newColor)
        {
            _playerVisual.SetColor(newColor);
        }

        private void HandleShoot()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            _weapon.Shoot(_cameraPoint.position, transform.forward);
        }

        [Server]
        private void HandleKill(float respawnTime)
        {
            RpcHandleKill(respawnTime);

            StartCoroutine(ResetLifeCoroutine(respawnTime));
        }

        [ClientRpc]
        private void RpcHandleKill(float respawnTime)
        {
            SetAlive(false);

            if (isLocalPlayer)
            {
                OnRespawnCooldownStarted?.Invoke(respawnTime);
            }
        }

        [ClientRpc]
        private void RpcRespawn()
        {
            transform.SetPositionAndRotation(_startPosition, Quaternion.identity);

            SetAlive(true);

            if (isLocalPlayer)
            {
                OnRespawnCooldownCompleted?.Invoke();
            }
        }

        [TargetRpc]
        private void TargetUpdateInventory(ItemType type, int count)
        {
            OnInventoryUpdated?.Invoke(type, count);
        }

        [TargetRpc]
        private void TargetTryUseAbsentItem(string itemName)
        {
            OnAbsentItemTried?.Invoke(itemName);
        }

        [Command]
        private void CmdPickItem(uint itemNetId)
        {
            if (NetworkServer.spawned.TryGetValue(itemNetId, out NetworkIdentity identity))
            {
                if (identity.gameObject.TryGetComponent<PickableItem>(out var pickedItem) &&
                    pickedItem.gameObject.activeSelf)
                {
                    var sqrDistance = (transform.position - pickedItem.transform.position).sqrMagnitude;

                    if (sqrDistance <= pickedItem.Config.PickSqrDistance)
                    {
                        var itemType = pickedItem.Config.Type;
                        _inventoryStorage.AddItem(itemType);

                        pickedItem.Pick();//make this as a method with [ClientRpc] or change item to ntworkbh

                        OnItemPicked?.Invoke(Name, pickedItem.Config.Name);
                    }
                    else
                    {
                        Debug.Log("try to pick item farther then pick distance");
                    }
                }
                else
                {
                    Debug.Log("try to pick netId object without PickableItem on it or inactive gameObject");
                }
                return;
            }

            Debug.Log("try to pick non-existing netId");
        }

        [Command]
        private void CmdHeal()
        {
            var type = ItemType.Medkit;

            if (_inventoryStorage.TryTakeItem(type))
            {
                if (_useMedkitCooldown &&
                    !_health.IsMaxHP)
                {
                    var config = (MedkitConfig)_pickItemsConfig.GetConfig(type);

                    _health.Heal(config.HealValue);

                    StartCoroutine(WaitMedkitUsing(config.UseWait));
                }
            }
        }

        [Command]
        private void CmdThrowGrenade()
        {
            var type = ItemType.Grenade;

            if (_inventoryStorage.TryTakeItem(type))
            {
                if (_useGrenadeCooldown)
                {
                    var config = (GrenadeConfig)_pickItemsConfig.GetConfig(type);

                    _grenadesService.Spawn(_grenadePoint);

                    StartCoroutine(WaitMedkitUsing(config.UseWait));
                }
            }
        }

        private void SetAlive(bool alive)
        {
            if (isLocalPlayer)
            {
                if (alive)
                {
                    _cameraFollower.SetPoint(_cameraPoint);
                }
                else
                {
                    _cameraFollower.SetSceneObservePoint();
                }

                _rigidbody.isKinematic = !alive;
            }

            _playerVisual.gameObject.SetActive(alive);
            _weapon.ShowVisual(alive);
            _collider.enabled = alive;
        }

        private IEnumerator ResetLifeCoroutine(float respawnTime)
        {
            while (respawnTime - (float)NetworkTime.time > 0)
            {
                yield return null;
            }

            _health.ResetHP();

            RpcRespawn();
        }

        private IEnumerator WaitMedkitUsing(WaitForSeconds wait)
        {
            _useMedkitCooldown = false;

            yield return wait;

            _useMedkitCooldown = true;
        }

        private IEnumerator WaitGrenadeUsing(WaitForSeconds wait)
        {
            _useGrenadeCooldown = false;

            yield return wait;

            _useGrenadeCooldown = true;
        }
    }
}