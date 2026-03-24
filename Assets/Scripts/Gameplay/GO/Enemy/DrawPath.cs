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
            Vector3[] corners = _agent.path.corners;
            _line.positionCount = corners.Length;

            _line.SetPositions(corners);
        }
    }
}