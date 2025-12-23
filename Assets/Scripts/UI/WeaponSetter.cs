using GameCore;
using UnityEngine;

namespace UI
{
    public sealed class WeaponSetter : MonoBehaviour
    {
        [SerializeField]
        private SliderSetter _speedSetter;

        [SerializeField]
        private SliderSetter _cooldownSetter;

        [SerializeField]
        private PlayerShooting _playerShooting;

        [SerializeField]
        private WeaponConfig _weaponConfig;

        private void Awake()
        {
            if (_weaponConfig == null)
            {
                Debug.LogError($"No weapon config assigned in {this.name} component");
            }
        }

        private void Start()
        {
            SetInitValues(_weaponConfig.FireCooldown, _weaponConfig.ProjectileSpeed);
        }

        private void OnEnable()
        {
            _cooldownSetter.ValueChanged += OnCooldownChanged;
            _speedSetter.ValueChanged += OnSpeedChanged;
        }

        private void OnDisable()
        {
            _cooldownSetter.ValueChanged -= OnCooldownChanged;
            _speedSetter.ValueChanged -= OnSpeedChanged;
        }

        private void SetInitValues(float cooldown, float speed)
        {
            _cooldownSetter.SetValue(cooldown);
            _speedSetter.SetValue(speed);
        }

        private void OnCooldownChanged(float cooldown)
        {
            _playerShooting.Cooldown = cooldown;
        }

        private void OnSpeedChanged(float speed)
        {
            _playerShooting.Speed = speed;
        }
    }
}