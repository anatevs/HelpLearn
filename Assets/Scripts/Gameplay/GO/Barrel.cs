using UnityEngine;

namespace Gameplay
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField]
        private HPComponent[] _parts;

        [SerializeField]
        private Rigidbody _bottomLidRB;

        [SerializeField]
        private float _destroyDelay = 4f;

        [SerializeField]
        private TouchDamageView _damageView;

        [SerializeField]
        private Color _activeRadiusColor;

        [SerializeField]
        private Color _inactiveRadiusColor;

        [SerializeField]
        private float _damageRadius = 4f;

        [SerializeField]
        private int _damage = 20;

        private bool _isExploded = false;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _damageView.SetRadius(_damageRadius);
            _damageView.Init(_activeRadiusColor, _inactiveRadiusColor);
            _damageView.SetActive();
        }

        private void OnEnable()
        {
            foreach (var part in _parts)
            {
                part.OnHPChanged += HandleCollision;
            }
        }

        private void OnDisable()
        {
            if (!_isExploded)
            {
                foreach (var part in _parts)
                {
                    part.OnHPChanged -= HandleCollision;
                }
            }
        }

        private void HandleCollision(int _)
        {
            MakeExplosion();
        }

        private void MakeExplosion()
        {
            if (!_isExploded)
            {
                Debug.Log("explosion");

                foreach (var part in _parts)
                {
                    part.OnHPChanged -= HandleCollision;
                }

                _bottomLidRB.isKinematic = false;

                Destroy(gameObject, _destroyDelay);

                DamageInRadius();

                _isExploded = true;

                _damageView.SetInactive();
            }
        }

        private void DamageInRadius()
        {
            var colliders = Physics.OverlapSphere(transform.position, _damageRadius);

            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent<HPComponent>(out var hp))
                    {
                        hp.TakeDamage(_damage);
                    }
                }
            }
        }
    }
}