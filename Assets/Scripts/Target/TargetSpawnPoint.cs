using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class TargetSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private float _startDelay;

        [SerializeField]
        private float _spawnPeriod;

        [SerializeField]
        private float _targetSpeed;

        private TargetSpawnService _targetService;

        private WaitForSeconds _spawnWait;

        public void Init(TargetSpawnService targetService)
        {
            _targetService = targetService;
        }

        private void Awake()
        {
            _spawnWait = new WaitForSeconds(_spawnPeriod);
        }

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(_startDelay);

            while (true)
            {
                _targetService.Spawn(transform, _targetSpeed);

                yield return _spawnWait;
            }
        }
    }
}