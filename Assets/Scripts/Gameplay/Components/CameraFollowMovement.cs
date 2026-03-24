using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class CameraFollowMovement : MonoBehaviour
    {
        [SerializeField]
        protected Transform _target;

        [SerializeField]
        private Transform[] _leftRightBorders;

        [SerializeField]
        private Transform[] _bottomTopBorders;

        protected Vector3 GetFollowingPosition(Transform followPoint)
        {
            var targetPoint = followPoint.position;

            var xPos = Mathf.Clamp(targetPoint.x, _leftRightBorders[0].position.x, _leftRightBorders[1].position.x);
            var zPos = Mathf.Clamp(targetPoint.z, _bottomTopBorders[0].position.z, _bottomTopBorders[1].position.z);

            targetPoint.x = xPos;
            targetPoint.z = zPos;

            return targetPoint;
        }
    }
}