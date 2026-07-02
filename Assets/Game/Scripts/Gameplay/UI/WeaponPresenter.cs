using Mirror;
using System.Collections;
using Gameplay;
using UnityEngine;

namespace UI
{
    public class WeaponPresenter : MonoBehaviour
    {
        [SerializeField]
        private WeaponView _view;

        private Weapon _weapon;

        private float _rechargeTime;

        public void Init(Weapon weapon)
        {
            _weapon = weapon;

            HandleChargeSet(_weapon.CurrentCharge);

            _weapon.OnChargeSet += HandleChargeSet;

            _rechargeTime = _weapon.Config.RechargeTime;

            _weapon.OnRecharchStarted += ShowRechargeBar;
            _weapon.OnRechargeEnded += HideRechargeBar;
        }

        public void OnDisable()
        {
            if (_weapon != null)
            {
                _weapon.OnChargeSet -= HandleChargeSet;

                _weapon.OnRecharchStarted -= ShowRechargeBar;
                _weapon.OnRechargeEnded -= HideRechargeBar;
            }
        }

        private void HandleChargeSet(int charge)
        {
            _view.SetCharge(charge.ToString());
        }

        private void ShowRechargeBar(float endTime)
        {
            _view.SetReloadProgress(0);
            _view.ShowCharge(false);

            StartCoroutine(ShowProgress(endTime));
        }

        private void HideRechargeBar()
        {
            _view.ShowCharge(true);
        }

        private IEnumerator ShowProgress(float endTime)
        {
            var progress = 0f;

            while (progress <= 1)
            {
                _view.SetReloadProgress(progress);
                progress = 1 - (float)(endTime - NetworkTime.time) / _rechargeTime;

                yield return null;
            }
        }
    }
}