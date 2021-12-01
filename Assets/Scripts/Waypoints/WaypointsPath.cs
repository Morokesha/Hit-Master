using UnityEngine;

namespace HitMaster.Game
{
    public class WaypointsPath : MonoBehaviour
    {
        public Waypoint[] WaypointsArray;

        private int _currentIndexPoint = 0;

        public Waypoint GetNextWaypointPos()
        {
            if (WaypointsArray.Length == 0)
            {
                return null;
            }

            Waypoint nextWaypoint = null;
            int nextindex = _currentIndexPoint + 1;

            if (nextindex <= WaypointsArray.Length)
            {
                nextWaypoint = WaypointsArray[_currentIndexPoint];

                _currentIndexPoint = nextindex;

                if (_currentIndexPoint > WaypointsArray.Length)
                {
                    return null;
                }
            }
            return nextWaypoint;
        }

    }
}

