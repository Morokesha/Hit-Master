using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitMaster.Enemies;
using HitMaster.Game;

namespace HitMaster.Player
{
    public class Scaner : MonoBehaviour
    {
        public event Action OnClearedZone;

        [SerializeField] private WaypointsPath _waypointsPath;

        private Enemy _nearbyEnemy;
        private Waypoint _currentWaypoint;

        private float _timeDelayScan = 0.3f;

        private bool _isActivatedScan = false;

        public void Activate(Waypoint point)
         {
            _currentWaypoint = point;

            _isActivatedScan = true;
            StartCoroutine(ScanPointToEnemiesCo());
         }

        public Enemy GetNearbyEnemy()
        {
            return _nearbyEnemy;
        }

        private IEnumerator ScanPointToEnemiesCo()
         {
            while (_isActivatedScan)
            {
                if (_currentWaypoint.Spawner !=null)
                {
                    if (_currentWaypoint.Spawner.GetCountEnemy() > 0)
                    {
                        GetInfoAboutEnemy();
                        yield return new WaitForSeconds(_timeDelayScan);
                    }
                    else
                    {
                        OnClearedZone?.Invoke();
                        _isActivatedScan = false;
                    }
                }
                else
                {
                    OnClearedZone?.Invoke();
                }
                
            }       
        }

        private void GetInfoAboutEnemy()
        {
            Enemy enemy = _currentWaypoint.Spawner.GetLastEnemy();

            if (enemy != null)
            {
                _nearbyEnemy = enemy;
            }
            else
            {
                return;
            }
        }
    }

}

