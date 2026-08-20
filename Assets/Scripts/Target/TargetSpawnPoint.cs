using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class TargetSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private SpawnPointData _data;

        private TargetSpawnService _targetService;

        private WaitForSeconds _spawnWait;

        public void Init(TargetSpawnService targetService)
        {
            _targetService = targetService;
        }

        public void SetupData(SpawnPointData data)
        {
            _data = data;
            _spawnWait = new WaitForSeconds(_data.SpawnPeriod);
        }

        private void Awake()
        {
            _spawnWait = new WaitForSeconds(_data.SpawnPeriod);
        }

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(_data.StartDelay);

            while (true)
            {
                _targetService.Spawn(transform, _data.TargetSpeed);

                yield return _spawnWait;
            }
        }
    }
}