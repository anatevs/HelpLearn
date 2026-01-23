using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class SpawnableTimerManager<T> : SpawnableManager<T> where T : SpawnableGO
    {
        [SerializeField]
        private float[] _spawnX = { -10, 10 };

        [SerializeField]
        private float[] _spawnZ = { -10, 10 };

        [SerializeField]
        private float[] _spawnPeriod = { 5f, 10f };

        private void Start()
        {
            StartCoroutine(SpawnProcess());
        }

        protected override void MakeAtSpawn(T obj)
        {
            var position = new Vector3(
                    RandomExtensions.RangeArray(_spawnX),
                    0,
                    RandomExtensions.RangeArray(_spawnZ));

            obj.transform.position = position;
        }

        private IEnumerator SpawnProcess()
        {
            while (gameObject.activeSelf)
            {
                Spawn(transform);

                yield return new WaitForSeconds(RandomExtensions.RangeArray(_spawnPeriod));
            }
        }
    }
}