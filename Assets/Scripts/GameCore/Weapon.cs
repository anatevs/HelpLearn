using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private WeaponBody _weaponBody;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            if (_animator == null)
            {
                Debug.LogWarning("No animator component on weapon");
            }
        }

        public void ShowBodyActive(bool isActive)
        {
            _weaponBody.ShowActive(isActive);
        }

        public void ActivateAnimator(bool isActive)
        {
            if (_animator != null)
            {
                _animator.enabled = isActive;
            }
        }

        [SerializeField]
        private void StopAnimator()
        {
            _animator.enabled = false;
        }
    }
}