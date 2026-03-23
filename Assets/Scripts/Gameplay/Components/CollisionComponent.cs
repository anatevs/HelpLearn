using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class CollisionComponent : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"collision with {collision.gameObject.name}");
        }
    }
}