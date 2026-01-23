using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class SpawnableManager<T> : MonoBehaviour where T : SpawnableGO
    {
        [SerializeField]
        private T _prefab;

        [SerializeField]
        private int _initPoolSize = 10;

        protected string _namePrefix = "";

        private Pool<T> _pool;

        private readonly Dictionary<string, T> _spawned = new();

        private int _spawnCount;

        protected virtual void Awake()
        {
            _pool = new Pool<T>(_prefab, _initPoolSize, transform);
        }

        public void Spawn(Transform spawnPoint)
        {
            if (!GameSingleton.Instance.GameManager.IsPaused)
            {
                var obj = _pool.Spawn(spawnPoint);

                _spawnCount++;
                obj.Name = $"{_namePrefix}{_spawnCount}";
                _spawned.Add(obj.Name, obj);

                MakeAtSpawn(obj);

                obj.OnUnspawned += Unspawn;
            }
        }

        protected virtual void MakeAtSpawn(T obj) { }

        private void Unspawn(string name)
        {
            if (_spawned.ContainsKey(name))
            {
                var obj = _spawned[name];

                obj.OnUnspawned -= Unspawn;

                _spawned.Remove(name);

                _pool.Unspawn(obj);

                MakeAtUnspawn(obj);
            }
        }

        protected virtual void MakeAtUnspawn(T obj) { }
    }
}