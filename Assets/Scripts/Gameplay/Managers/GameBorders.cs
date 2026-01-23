using UnityEngine;

namespace Gameplay
{
    public class GameBorders : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _udlrBorders = new Transform[4];

        public bool IsInBorders(Vector3 checking)
        {
            return (checking.z < _udlrBorders[0].position.z &&
                checking.z > _udlrBorders[1].position.z &&
                checking.x > _udlrBorders[2].position.x &&
                checking.x < _udlrBorders[3].position.x);
        }
    }
}