using Mirror;
using System;
using UnityEngine;

namespace Gameplay
{
    public class Health : NetworkBehaviour
    {
        public event Action<int, int> OnHealthChange;

        public event Action<float> OnKilled;

        public int HP => _hp;

        public PlayerHealthConfig HPConfig => _healthConfig;

        [SerializeField]
        private PlayerHealthConfig _healthConfig;

        [SyncVar(hook = nameof(HookChangeHP))]
        private int _hp = 0;

        private void Awake()
        {
            syncDirection = SyncDirection.ServerToClient;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            ResetHP();
        }

        [Server]
        public void TakeDamage(int damage)
        {
            ChangeHP(-damage);

            if (_hp == 0)
            {
                var respawnTime = (float)NetworkTime.time + _healthConfig.RespawnDelay;

                OnKilled?.Invoke(respawnTime);
            }
        }

        [Server]
        public void Heal(int hp)
        {
            ChangeHP(hp);
        }

        [Server]
        public void ResetHP()
        {
            ChangeHP(_healthConfig.InitHP);
        }

        [Server]
        private void ChangeHP(int value)
        {
            var oldValue = _hp;

            _hp += value;

            _hp = Mathf.Clamp(_hp, 0, _healthConfig.MaxHP);
        }

        private void HookChangeHP(int oldValue, int newValue)
        {
            OnHealthChange?.Invoke(oldValue, _hp);
        }
    }
}