using EventBusNamespace;
using GameManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Gameplay
{
    public sealed class EnemySpawnService : MonoBehaviour,
        IResetable
    {
        [SerializeField]
        private EnemySpawner _spawner;

        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _enemiesTransform;

        private EnemySpawnConfig _config;

        private EventBus _eventBus;

        private Player _player;

        private PatrolLocation[] _locations;

        private readonly Dictionary<string, Pool<Enemy>> _pools = new();

        private readonly Dictionary<EnemyStrategyType, Action<Enemy>> _setupActions = new();

        private WaitForSeconds _spawnWait;

        private Coroutine _spawnCoroutine;

        private ProjectileSpawnService _projectileService;


        [Inject]
        public void Construct(EventBus eventBus,
            ProjectileSpawnService projectileService)
        {
            _eventBus = eventBus;
            _projectileService = projectileService;

            _eventBus.Subscribe<EnemyKilledEvent>(Unspawn);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<EnemyKilledEvent>(Unspawn);
        }

        public void InitOnScene(EnemySpawnConfig config, Player player, PatrolLocation[] locations)
        {
            _config = config;

            _player = player;

            _locations = locations;

            _spawner.Init(_pools, _config.PoolInitCount, _poolTransform);

            _setupActions.Add(EnemyStrategyType.Attack, SetupAttacking);
            _setupActions.Add(EnemyStrategyType.Patrol, SetupPatrolling);

            _spawnWait = new WaitForSeconds(_config.SpawnPeriod);
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

        private void Unspawn(EnemyKilledEvent e)
        {
            var enemy = e.Value;

            Unspawn(enemy);
        }

        private void Unspawn(Enemy enemy)
        {
            enemy.SetStrategy(null);

            enemy.transform.position = Vector3.zero;

            enemy.Clear();

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

            enemy.Init(new AttackStrategy(enemy, _player.transform), _projectileService, _eventBus);

            var setup = _setupActions[enemy.Config.StartStrategy];

            setup.Invoke(enemy);

            _eventBus.RaiseEvent(new EnemySpawnedEvent(enemy));
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