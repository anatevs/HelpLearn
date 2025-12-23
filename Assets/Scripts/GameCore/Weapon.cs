using UnityEngine;

namespace GameCore
{
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField]
        private WeaponBody _weaponBody;

        private Animator _animator;

        private const string _recoilTrigger = "Recoil";

        private int _recoilID;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            if (_animator == null)
            {
                Debug.LogWarning("No animator component on weapon");
            }
            else
            {
                _recoilID = Animator.StringToHash(_recoilTrigger);
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
                _animator.SetTrigger(_recoilID);
            }
        }
    }
}