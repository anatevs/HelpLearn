using EventBusNamespace;
using GameManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class EnemySpawnService : DDOLClass<EnemySpawnService>
    {
        [SerializeField]
        private EnemySpawnConfig _config;

        [SerializeField]
        private EnemySpawner _spawner;

        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _enemiesTransform;

        private Player _player;

        private PatrolLocation[] _locations;

        private readonly Dictionary<string, Pool<Enemy>> _pools = new();

        private readonly Dictionary<EnemyStrategyType, Action<Enemy>> _setupActions = new();

        private WaitForSeconds _spawnWait;

        private Coroutine _spawnCoroutine;

        private void OnDisable()
        {
            EventBus.Unsubscribe<EnemyKilledEvent>(Unspawn);
        }

        public void Init(Player player, PatrolLocation[] locations)
        {
            _player = player;

            _locations = locations;

            _spawner.Init(_pools, _config.PoolInitCount, _poolTransform);

            _setupActions.Add(EnemyStrategyType.Attack, SetupAttacking);
            _setupActions.Add(EnemyStrategyType.Patrol, SetupPatrolling);

            _spawnWait = new WaitForSeconds(_config.SpawnPeriod);

            Reset();
        }

        public void Reset()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }

            if (_enemiesTransform.childCount > 0)
            {
                var activeEnemies = _enemiesTransform.GetComponentsInChildren<Enemy>();

                foreach (var enemy in activeEnemies)
                {
                    Unspawn(enemy);
                }
            }

            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        public void Unspawn(EnemyKilledEvent e)
        {
            var enemy = e.Value;

            Unspawn(enemy);
        }

        private void Unspawn(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);

            enemy.SetStrategy(null);

            enemy.transform.position = Vector3.zero;

            _pools[enemy.Config.Name].Unspawn(enemy);
        }

        private IEnumerator SpawnCoroutine()
        {
            while (gameObject.activeSelf)
            {
                SpawnRandom();

                yield return _spawnWait;
            }
        }

        private void SpawnRandom()
        {
            var enemy = _spawner.GetRandomEnemy(_pools, _enemiesTransform);

            enemy.Init(new AttackStrategy(enemy, _player.transform));

            var setup = _setupActions[enemy.Config.StartStrategy];

            setup.Invoke(enemy);

            EventBus.RaiseEvent(new EnemySpawnedEvent(enemy));

            EventBus.Subscribe<EnemyKilledEvent>(Unspawn);
        }

        private void SetupAttacking(Enemy enemy)
        {
            var pos = _config.GetSpawnPos();

            var startBehaviour = new AttackStrategy(enemy, _player.transform);

            SetupEnemy(enemy, startBehaviour, pos);
        }

        private void SetupPatrolling(Enemy enemy)
        {
            var location = _locations[UnityEngine.Random.Range(0, _locations.Length)];

            var pos = location.CentralPoint.position;

            var startBehaviour = new PatrolStrategy(enemy, location.Points);

            SetupEnemy(enemy, startBehaviour, pos);
        }

        private void SetupEnemy(Enemy enemy, EnemyStrategy behaviour, Vector3 pos)
        {
            enemy.SetStrategy(behaviour);

            enemy.transform.position = pos;

            enemy.gameObject.SetActive(true);
        }
    }
}