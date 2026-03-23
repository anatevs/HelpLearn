using System.Collections;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider))]
    public sealed class TouchDamage : MonoBehaviour
    {
        [SerializeField]
        private TouchDamageView _damageView;

        private HPComponent _targetHP;

        private TouchDamageConfig _config;

        private bool _isCooldown = false;
        private WaitForSeconds _cooldownWait;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Init(HPComponent targetHP, TouchDamageConfig config)
        {
            _targetHP = targetHP;
            _config = config;

            transform.localScale = new Vector3(config.DamageRadius, 1, config.DamageRadius);
            _cooldownWait = new WaitForSeconds(_config.DamageCooldown);

            _damageView.Init(_config.DamageActiveColor, _config.DamageInactiveColor);
            _damageView.SetActive();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isCooldown && other.gameObject == _targetHP.gameObject)
            {
                _targetHP.TakeDamage(_config.Damage);
                _isCooldown = true;
                _collider.enabled = false;
                _damageView.SetInactive();

                StartCoroutine(MakeCooldown());
            }
        }

        private IEnumerator MakeCooldown()
        {
            yield return _cooldownWait;
            _isCooldown = false;
            _collider.enabled = true;
            _damageView.SetActive();
        }
    }
}