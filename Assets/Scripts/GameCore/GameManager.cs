using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;

namespace GameCore
{
    public sealed class GameManager : MonoBehaviour
    {
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

        [SerializeField]
        private TMP_Text _countText;

        private float _spawnDelay = 4f;

        private int _rangeSize = 2;

        private int _targetsCount = 0;

        private void Start()
        {
            SetCountText();

            if (_xPosRange.Length != _rangeSize ||
                _yPosRange.Length != _rangeSize ||
                _zPosRange.Length != _rangeSize)
            {
                new WarningException(
                    $"Some of the ranges (x, y, z) for random target" +
                    $" position is not has {_rangeSize} values. Spawn will not start");

                return;
            }

            StartCoroutine(SpawnTargets());
        }

        private IEnumerator SpawnTargets()
        {
            while (gameObject.activeSelf)
            {
                SpawnTarget();

                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        private void SpawnTarget()
        {
            var pos = new Vector3
                (
                Random.Range(_xPosRange[0], _xPosRange[1]),
                Random.Range(_yPosRange[0], _yPosRange[1]),
                Random.Range(_zPosRange[0], _zPosRange[1])
                );

            Instantiate(_targetPrefab, pos, Quaternion.identity, _targetsParent);

            _targetsCount++;

            SetCountText();
        }

        private void SetCountText()
        {
            _countText.text = _targetsCount.ToString();
        }
    }
}