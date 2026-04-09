using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "SpawnRoomConfig",
        menuName = "Configs/SpawnRoom")]
    public sealed class SpawnRoom : ScriptableObject
    {
        public IReadOnlyList<Transform> Locations => _spawnPoints;

        [SerializeField, HideInInspector]
        private List<Transform> _spawnPoints = new();

        [SerializeField]
        private Transform _addLocation;

        [SerializeField]
        private Transform _removeLocation;

        public void AddLocation()
        {
            if (_addLocation == null)
            {
                return;
            }

            _spawnPoints.Add(_addLocation);
        }

        public void RemoveLocation()
        {
            if (_addLocation == null)
            {
                return;
            }

            _spawnPoints.Remove(_removeLocation);
        }
    }
}