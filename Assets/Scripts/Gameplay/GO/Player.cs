using Events;
using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(RotationComponent))]
    [RequireComponent(typeof(ShootComponent))]
    [RequireComponent(typeof(HPComponent))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private InputManager _input;

        [SerializeField]
        private DetectEnemy _detectEnemy;

        private MoveComponent _characterMove;

        private RotationComponent _rotation;

        private ShootComponent _shoot;

        private HPComponent _hp;

        private Vector3 _look;

        private void Awake()
        {
            _characterMove = GetComponent<MoveComponent>();
            _rotation = GetComponent<RotationComponent>();
            _shoot = GetComponent<ShootComponent>();
            _hp = GetComponent<HPComponent>();
        }

        private void OnEnable()
        {
            _input.OnShot += Shoot;

            _detectEnemy.OnNewEnemy += SetNewActiveEnemy;

            _hp.OnDestroyed += EndGame;
        }

        private void OnDisable()
        {
            _input.OnShot -= Shoot;

            _detectEnemy.OnNewEnemy -= SetNewActiveEnemy;

            _hp.OnDestroyed -= EndGame;
        }

        private void Update()
        {
            _characterMove.MoveUpdate(_input.Move);

            _look = _input.MousePoint - transform.position;
            _look.y = 0;

            _rotation.RotateUpdate(_look);
        }

        private void SetNewActiveEnemy(Enemy enemy)
        {
            enemy.SetTarget(this);

            var e = new GameEvent(Events.EventType.BattleStart,
                DateTime.Now,
                $"Battle between player and {enemy.Name} started");
            GameSingleton.Instance.EventManager.TriggerEvent(e);
        }

        private void Shoot()
        {
            _shoot.Shoot();
        }

        private void EndGame()
        {
            GameSingleton.Instance.GameManager.RestartGame();
        }
    }
}