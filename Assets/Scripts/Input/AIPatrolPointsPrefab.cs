using UnityEngine;

namespace Input
{
    public class AIPatrolPointsPrefab : MonoBehaviour
    {
        public Transform[] PatrolPoints => _patrolPoints;

        [SerializeField]
        private Transform[] _patrolPoints;
    }
}