using UnityEngine;

namespace Gameplay
{
    public class WeaponTracer : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        private void Update()
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.forward);
        }
    }
}