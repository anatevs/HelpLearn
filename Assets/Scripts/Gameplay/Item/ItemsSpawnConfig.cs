using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ItemsSpawnConfig",
        menuName = "Configs/ItemsSpawn")]
    public sealed class ItemsSpawnConfig : ScriptableObject
    {
        public WaitForSeconds SpawnWait => _spawnWait;

        public float SpawnPeriod => _spawnPeriod;

        public string[] ItemNames => _names;

        [SerializeField]
        private Item[] _prefabs;

        [SerializeField]
        private float _spawnPeriod;

        [SerializeField]
        private float[] _xRange = new float[2];

        [SerializeField]
        private float[] _zRange = new float[2];

        private WaitForSeconds _spawnWait;

        private readonly Dictionary<string, Item> _prefabsDict = new();

        private string[] _names;

        public void Init()
        {
            _spawnWait = new WaitForSeconds(_spawnPeriod);

            _names = new string[_prefabs.Length];

            for (int i = 0; i < _prefabs.Length; i++)
            {
                _prefabsDict.Add(_prefabs[i].Name, _prefabs[i]);
                _names[i] = _prefabs[i].Name;
            }
        }

        public Item GetPrefab(string name)
        {
            return _prefabsDict[name];
        }

        public Vector3 GetRandomPosition()
        {
            var x = Random.Range(_xRange[0], _xRange[1]);
            var z = Random.Range(_zRange[0], _zRange[1]);

            return new Vector3(x, 0, z);
        }
    }
}