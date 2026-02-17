using System;
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

        [SerializeField]
        private GameConfig _gameConfig;

        private string[] _enemyNames;

        private readonly Dictionary<string, Pool<Enemy>> _pools = new();

        private Action<Enemy>[] _setupActions;

        private void Awake()
        {
            Init();

            SpawnRandom();
        }

        public void Unspawn(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);

            enemy.SetStrategy(null);

            _pools[enemy.Config.Name].Unspawn(enemy);
        }

        private void SpawnRandom()
        {
            var name = _enemyNames[UnityEngine.Random.Range(0, _enemyNames.Length)];

            Debug.Log(name);

            var enemy = _pools[name].Spawn(_enemiesTransform);

            var setup = _setupActions[UnityEngine.Random.Range(0, _setupActions.Length)];

            Debug.Log(setup);

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
            var startBehaviour = new AttackBehaviour(enemy, _player.transform);

            enemy.SetStrategy(startBehaviour);

            var pos = _config.GetSpawnPos();

            enemy.transform.position = pos;

            enemy.gameObject.SetActive(true);
        }

        private void SetupPatrolling(Enemy enemy)
        {
            var location = _locations[UnityEngine.Random.Range(0, _locations.Length)];

            var startBehaviour = new PatrolBehaviour(enemy, location._points);

            enemy.SetStrategy(startBehaviour);

            enemy.transform.position = location._points[0].position;

            enemy.gameObject.SetActive(true);
        }
    }

    [Serializable]
    public struct PatrolLocation
    {
        public Transform[] _points;
    }
}