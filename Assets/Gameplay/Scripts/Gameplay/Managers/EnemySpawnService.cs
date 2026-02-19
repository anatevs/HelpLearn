using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EnemySpawnService : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnConfig _config;

        [SerializeField]
        private Transform _poolTransform;

        [SerializeField]
        private Transform _enemiesTransform;

        [SerializeField]
        private PatrolLocation[] _locations;

        [SerializeField]
        private Player _player;

        //[SerializeField]
        //private ProjectileSpawnService _projectileSpawn;

        private string[] _enemyNames;

        private readonly Dictionary<string, Pool<Enemy>> _pools = new();

        private Action<Enemy>[] _setupActions;

        private WaitForSeconds _spawnWait;

        private void Awake()
        {
            Init();

            _spawnWait = new WaitForSeconds(_config.SpawnPeriod);

            StartCoroutine(SpawnCoroutine());
        }

        public void Unspawn(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);

            enemy.SetStrategy(null);

            _pools[enemy.Config.Name].Unspawn(enemy);

            enemy.transform.position = Vector3.zero;
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

            enemy.Init(new AttackBehaviour(enemy, _player.transform));//, _projectileSpawn);

            var setup = _setupActions[UnityEngine.Random.Range(0, _setupActions.Length)];

            setup.Invoke(enemy);
        }

        private void Init()
        {
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
        }

        private void SetupAttacking(Enemy enemy)
        {
            var pos = _config.GetSpawnPos();

            var startBehaviour = new AttackBehaviour(enemy, _player.transform);

            SetupEnemy(enemy, startBehaviour, pos);
        }

        private void SetupPatrolling(Enemy enemy)
        {
            var location = _locations[UnityEngine.Random.Range(0, _locations.Length)];

            var pos = location.Points[0].position;

            var startBehaviour = new PatrolBehaviour(enemy, location.Points);

            SetupEnemy(enemy, startBehaviour, pos);
        }

        private void SetupEnemy(Enemy enemy, EnemyBehaviour behaviour, Vector3 pos)
        {
            enemy.SetStrategy(behaviour);

            enemy.transform.position = pos;

            enemy.gameObject.SetActive(true);
        }
    }

    [Serializable]
    public struct PatrolLocation
    {
        public Transform[] Points;
    }
}