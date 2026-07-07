using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ItemSpawnPointsStorage :
        IDisposable
    {
        private readonly ItemSpawnPoint[] _spawnPoints;

        private readonly Dictionary<ItemType, List<ItemSpawnPoint>> _freePoints = new();

        public ItemSpawnPointsStorage(ItemSpawnPoint[] spawnPoints)
        {
            _spawnPoints = spawnPoints;

            foreach (var point in _spawnPoints)
            {
                if (!_freePoints.ContainsKey(point.ItemType))
                {
                    _freePoints.Add(point.ItemType, new List<ItemSpawnPoint>());
                }

                _freePoints[point.ItemType].Add(point);

                point.OnItemPicked += AddToFreePoints;
            }
        }

        public void Dispose()
        {
            foreach (var point in _spawnPoints)
            {
                if (point != null)
                {
                    point.OnItemPicked -= AddToFreePoints;
                }
            }
        }

        public bool TryGetRandomPoint(ItemType type, out ItemSpawnPoint point)
        {
            if (!_freePoints.ContainsKey(type) || _freePoints[type].Count <= 0)
            {
                point = null;
                return false;
            }

            int index = UnityEngine.Random.Range(0, _freePoints[type].Count);

            point = _freePoints[type][index];

            _freePoints[type].RemoveAt(index);

            return true;
        }

        private void AddToFreePoints(ItemSpawnPoint point)
        {
            if (!_freePoints.ContainsKey(point.ItemType))
            {
                Debug.LogError($"item spawn points service does not contain points for type {point.ItemType}");
            }

            _freePoints[point.ItemType].Add(point);
        }
    }
}