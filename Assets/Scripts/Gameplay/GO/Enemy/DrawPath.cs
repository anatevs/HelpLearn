using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(LineRenderer))]
    public class DrawPath : MonoBehaviour
    {
        private LineRenderer _line;
        private NavMeshAgent _agent;
        private Vector3[] _corners = new Vector3[50];

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _line = GetComponent<LineRenderer>();

            _line.positionCount = 0;
        }

        private void Update()
        {
            if (_agent.hasPath)
            {
                DrawAgentPath();
            }
            else
            {
                _line.positionCount = 0;
            }
        }

        void DrawAgentPath()
        {
            _line.positionCount = _agent.path.corners.Length;
            _corners = _agent.path.corners;

            _line.SetPositions(_corners);
        }
    }
}