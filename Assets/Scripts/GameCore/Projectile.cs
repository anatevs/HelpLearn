using UnityEngine;

namespace GameCore
{
    public sealed class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5f;

        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}