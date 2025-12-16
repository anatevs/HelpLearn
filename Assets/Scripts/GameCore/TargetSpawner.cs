using System.ComponentModel;
using UnityEngine;

namespace GameCore
{
    public class TargetSpawner : MonoBehaviour
    {
        public int TargetsCount => _targetsCount;

        [SerializeField]
        private float[] _xPosRange = new float[2];

        [SerializeField]
        private float[] _yPosRange = new float[2];

        [SerializeField]
        private float[] _zPosRange = new float[2];

        [SerializeField]
        private Target _targetPrefab;

        [SerializeField]
        private Transform _targetsParent;

        private int _rangeSize = 2;

        private bool _canSpawn = true;

        private int _targetsCount = 0;

        private void Start()
        {
            if (_xPosRange.Length != _rangeSize ||
                _yPosRange.Length != _rangeSize ||
                _zPosRange.Length != _rangeSize)
            {
                _canSpawn = false;

                new WarningException(
                    $"Some of the ranges (x, y, z) for random target" +
                    $" position is not has {_rangeSize} values. Spawn will not start");

                return;
            }
        }

        public void SpawnTarget()
        {
            if (_canSpawn)
            {
                var pos = new Vector3
                    (
                    Random.Range(_xPosRange[0], _xPosRange[1]),
                    Random.Range(_yPosRange[0], _yPosRange[1]),
                    Random.Range(_zPosRange[0], _zPosRange[1])
                    );

                Instantiate(_targetPrefab, pos, Quaternion.identity, _targetsParent);

                _targetsCount++;
            }
        }
    }
}