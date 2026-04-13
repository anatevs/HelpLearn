using System;
using UnityEngine;

namespace Gameplay
{
    public class FallComponent : MonoBehaviour
    {
        public event Action OnFell;

        [SerializeField]
        private float _fallLevel = -0.5f;

        public void CheckFallUpd()
        {
            if (transform.position.y < _fallLevel)
            {
                OnFell?.Invoke();
            }
        }
    }
}