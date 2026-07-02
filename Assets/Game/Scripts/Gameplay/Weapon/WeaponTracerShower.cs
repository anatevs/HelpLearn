using Mirror;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class WeaponTracerShower : NetworkBehaviour
    {
        [SerializeField]
        private GameWeaponsConfig _weaponsConfig;

        private void Awake()
        {
            _weaponsConfig.Init();
        }

        [ClientRpc]
        public void ShowTrace(Vector3 startPoint, Vector3 endPoint, string weaponName)
        {
            StartCoroutine(ShowTraceCoroutine(startPoint, endPoint, weaponName));
        }

        private IEnumerator ShowTraceCoroutine(Vector3 startPoint, Vector3 endPoint, string weaponName)
        {
            var direction = endPoint - startPoint;
            direction.y = 0;

            var weaponConfig = _weaponsConfig.GetConfig(weaponName);
            var trace = SpawnTrace(weaponConfig, startPoint, direction.normalized);

            var sqrPath = direction.sqrMagnitude;

            trace.gameObject.SetActive(true);

            while ((trace.transform.position - startPoint).sqrMagnitude <= sqrPath)
            {
                yield return null;
            }

            UnspawnTrace(trace);
        }

        private WeaponTracer SpawnTrace(WeaponConfig weaponConfig, Vector3 startPoint, Vector3 direction)
        {
            var trace = Instantiate(weaponConfig.TracerPrefab, transform);

            trace.gameObject.SetActive(false);

            trace.transform.position = startPoint;
            trace.transform.forward = direction;

            return trace;
        }

        private void UnspawnTrace(WeaponTracer trace)
        {
            Destroy(trace.gameObject);
        }
    }
}