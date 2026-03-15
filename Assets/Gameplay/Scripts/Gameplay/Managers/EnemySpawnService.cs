using EventBusNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class EnemySpawnService : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnConfig _config;

        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _enemiesTransform;

        [SerializeField]
        private EventBus _eventBus;

        private Player _player;

        private PatrolLocation[] _locations;

        private string[] _enemyNames;

        private readonly Dictionary<string, Pool<Enemy>> _pools = new();

        private Action<Enemy>[] _setupActions;

        private WaitForSeconds _spawnWait;

        private Coroutine _spawnCoroutine;

        private ProjectileSpawnService _projectileService;

        private void OnEnable()
        {
            _eventBus.Subscribe<EnemyKilledEvent>(Unspawn);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<EnemyKilledEvent>(Unspawn);
        }

        public void Construct(Player player, PatrolLocation[] locations,
            ProjectileSpawnService projectileService)
        {
            _player = player;

            _locations = locations;

            _projectileService = projectileService;

            _enemyNames = new string[_config.Prefabs.Length];

            for (int i = 0; i < _config.Prefabs.Length; i++)
            {
                var enemy = _config.Prefabs[i];

                _pools.Add(enemy.Config.Name, new Pool<Enemy>(enemy, _config.PoolInitCount, _poolTransform));

                _enemyNames[i] = enemy.Config.Name;
            }

            _setupActions = new Action<Enemy>[]
            {
                SetupAttacking,
                SetupPatrolling
            };

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

        private void Unspawn(EnemyKilledEvent e)
        {
            var enemy = e.Value;

            Unspawn(enemy);
        }

        private void Unspawn(Enemy enemy)
        {
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
            var name = _enemyNames[UnityEngine.Random.Range(0, _enemyNames.Length)];

            var enemy = _pools[name].Spawn(_enemiesTransform);

            enemy.Init(new AttackStrategy(enemy, _player.transform), _projectileService);

            var setup = _setupActions[UnityEngine.Random.Range(0, _setupActions.Length)];

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