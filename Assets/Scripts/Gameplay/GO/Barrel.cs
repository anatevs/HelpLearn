using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField]
        private HPComponent[] _parts;

        [SerializeField]
        private Rigidbody _bottomLidRB;

        [SerializeField]
        private float _destroyDelay = 4f;

        private void OnEnable()
        {
            foreach (var part in _parts)
            {
                part.OnHPChanged += HandleCollision;
            }
        }

        private void OnDisable()
        {
            foreach (var part in _parts)
            {
                part.OnHPChanged -= HandleCollision;
            }
        }

        private void HandleCollision(int _)
        {
            MakeExplosion();
        }

        private void MakeExplosion()
        {
            _bottomLidRB.isKinematic = false;

            Destroy(gameObject, _destroyDelay);
        }
    }
}