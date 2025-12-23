using GameCore;
using GameManagement;
using UnityEngine;

namespace UI
{
    public sealed class WeaponDataManager : MonoBehaviour
    {
        [SerializeField]
        private SliderSetter _speedSetter;

        [SerializeField]
        private SliderSetter _cooldownSetter;

        [SerializeField]
        private PlayerShooting _playerShooting;

        [SerializeField]
        private SaveLoad_WeaponParams _saveLoad;

        private void Start()
        {
            _cooldownSetter.SetValue(_saveLoad.Cooldown);
            _speedSetter.SetValue(_saveLoad.Speed);
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

            _saveLoad.Save();
        }

        private void OnCooldownChanged(float cooldown)
        {
            _playerShooting.Cooldown = cooldown;
            _saveLoad.Cooldown = cooldown;
        }

        private void OnSpeedChanged(float speed)
        {
            _playerShooting.Speed = speed;
            _saveLoad.Speed = speed;
        }
    }
}