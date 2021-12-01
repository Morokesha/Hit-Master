using HitMaster.Enemies;
using HitMaster.Game;
using System;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

namespace HitMaster.Player
{
    public class MovementPlayer : MonoBehaviour
    {
        public event Action<Waypoint> ReachedPoint;

        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private WaypointsPath _waypointsPath;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _moveSpeed;


        private Scaner _scaner;
        private PlayerShooting _playerShoting;
        private Waypoint _currentPoint;
        private NavMeshAgent _navMeshAgent;

        private readonly int _run = Animator.StringToHash("Run");
        private readonly float _speedRotate = 0.3f;

        private bool _gameStarted = false;
        private bool _canTurned;
        private bool _canMove = true;

        private void Awake()
        {
            _scaner = GetComponent<Scaner>();
            _playerShoting = GetComponent<PlayerShooting>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            _scaner.OnClearedZone += ClearedZone;
            _inputSystem.TochedScreen += TouchScreen;
        }

        private void Update()
        {
            if (_gameStarted == true)
            {
                if (_canMove)
                {
                    if (_navMeshAgent.remainingDistance < 0.4f && !_navMeshAgent.pathPending)
                    {
                        ReachedPoint?.Invoke(_currentPoint);
                        _animator.SetBool(_run, false);
                        _canMove = false;
                    }
                }
                TurnOnTheEnemy();
            }      
        }

        #region Events Methods
        private void ClearedZone()
        {
            _canMove = true;
            MoveTo();
        }

        private void TouchScreen()
        {
            if (_gameStarted == false && _canMove ==true)
            {
                _gameStarted = true;
                MoveTo();
            }

        }
        private void OnDisable()
        {
            _inputSystem.TochedScreen -= TouchScreen;
            _scaner.OnClearedZone -= ClearedZone;
        }
        #endregion

        #region Move and Turns
        private void TurnOnTheEnemy()
        {
            _canTurned = _playerShoting.AllowedShot;

            if (_canTurned == true)
            {
                Enemy enemy = _scaner.GetNearbyEnemy();
                transform.DOLookAt(enemy.transform.position, _speedRotate);

                if (enemy == null)
                {
                    return;
                }

            }
        }

        private void MoveTo()
        {
            MoveToNextPoint();
        }

        private void MoveToNextPoint()
        {
            if (_canMove == true)
            {
                Waypoint waypoint = _waypointsPath.GetNextWaypointPos();


                if (waypoint != null)
                {
                    _currentPoint = waypoint;

                    _navMeshAgent.SetDestination(_currentPoint.transform.position);
                    _navMeshAgent.speed = _moveSpeed;

                    _animator.SetBool(_run, true);
                }
                else
                {
                    StopMove();
                }
            }
        }
        
        private void StopMove()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.speed = 0;

            _animator.SetBool(_run, false);
        }
        #endregion
    }
}

