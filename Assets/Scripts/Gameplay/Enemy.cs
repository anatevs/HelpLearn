using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(RotationComponent))]
    [RequireComponent(typeof(ShootComponent))]
    [RequireComponent(typeof(HPComponent))]
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDestroyed;

        public bool IsAttack => _isAttack;

        [SerializeField]
        private float _changeDirectionDuration = 3f;

        [SerializeField]
        private float _shootPeriod = 2f;

        private Player _target = null;

        private bool _isAttack = false;

        private MoveComponent _characterMove;

        private RotationComponent _rotation;

        private ShootComponent _shoot;

        private HPComponent _hp;

        private Vector3 _direction;

        private void Awake()
        {
            _characterMove = GetComponent<MoveComponent>();
            _rotation = GetComponent<RotationComponent>();
            _shoot = GetComponent<ShootComponent>();
            _hp = GetComponent<HPComponent>();
        }

        private void OnEnable()
        {
            StartCoroutine(RandomDirection());

            _hp.OnDestroyed += DestroyEnemy;
        }

        private void OnDisable()
        {
            _hp.OnDestroyed -= DestroyEnemy;
        }

        private void Update()
        {
            if (_isAttack)
            {
                _direction = 
                    (_target.transform.position - transform.position).normalized;
                _direction.y = 0;
            }

            _characterMove.MoveUpdate(_direction);
            _rotation.RotateUpdate(_direction);
        }

        public void SetTarget(Player target)
        {
            _target = target;
            _isAttack = true;

            StartCoroutine(Shooting());
        }

        public void ResetEnemy()
        {
            _target = null;
            _isAttack = false;
            _hp.Reset();
        }

        private void DestroyEnemy()
        {
            OnDestroyed?.Invoke(this);
        }

        private IEnumerator RandomDirection()
        {
            while (!_isAttack)
            {
                var randomDirection = UnityEngine.Random.insideUnitCircle;

                _direction = new Vector3(randomDirection.x, 0, randomDirection.y).normalized;

                yield return new WaitForSeconds(_changeDirectionDuration);
            }
        }

        private IEnumerator Shooting()
        {
            while (gameObject.activeSelf)
            {
                _shoot.Shoot();

                yield return new WaitForSeconds(_shootPeriod);
            }
        }
    }
}