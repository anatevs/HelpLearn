using UnityEngine;

namespace Gameplay
{
    public sealed class PatrolZones : MonoBehaviour
    {
        public PatrolLocation[] Locations => _locations;

        private PatrolLocation[] _locations;

        public void Init()
        {
            _locations = new PatrolLocation[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                var zone = transform.GetChild(i);
                var location = new PatrolLocation();

                location.CentralPoint = zone;

                location.Points = new Transform[zone.childCount];

                for (int j = 0; j < zone.childCount; j++)
                {
                    location.Points[j] = zone.GetChild(j).transform;
                }

                _locations[i] = location;
            }
        }
    }
}