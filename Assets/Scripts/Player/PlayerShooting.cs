using System.Collections;
using UnityEngine;
using HitMaster.Game;

namespace HitMaster.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Scaner _scaner;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private MovementPlayer _movementPlayer;
        [SerializeField] private BulletsPool _bulletsPool;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _weaponPosition;

        public bool AllowedShot => _allowedShot;

        private readonly int _shootAnim = Animator.StringToHash("Shoot");
        private float _shotDelay = 0.6f;
        private bool _canShoot = true;
        private bool _allowedShot = false;

        private void Start()
        {
            _movementPlayer.ReachedPoint += ReachedPoint;
            _inputSystem.TochedScreen += TouchedScreen;
            _scaner.OnClearedZone += ClearZone;
        }

        private void ReachedPoint(Waypoint waypoint)
        {
            _allowedShot = true;
            _scaner.Activate(waypoint);
        }

        private void TouchedScreen()
        {
            if (_allowedShot == true && _canShoot == true)
            {
                Shooting();
            }    
        }

        private void ClearZone()
        {
            _allowedShot = false;
        }

        private void Shooting()
        {
            Vector2 mousePosition = _inputSystem.GetMousePositionOnScreen();

            Ray ray = _camera.ScreenPointToRay(mousePosition);

            float distanceRay = 200f;

            if (Physics.Raycast(ray, out RaycastHit hit, distanceRay))
            {
                StartCoroutine(ShotCo(hit.point));
            }
        }

        private IEnumerator ShotCo(Vector3 targetPos)
        {
            _canShoot = false;

            _animator.SetTrigger(_shootAnim);
            
            Bullet bullet = _bulletsPool.GetBulletActve(_weaponPosition.transform.position);
            bullet.ShotOnTarget(targetPos);

            yield return new WaitForSeconds(_shotDelay);

            _canShoot = true;
        }

        private void OnDisable()
        {
            _movementPlayer.ReachedPoint -= ReachedPoint;
            _inputSystem.TochedScreen -= TouchedScreen;
        }
    }
}

